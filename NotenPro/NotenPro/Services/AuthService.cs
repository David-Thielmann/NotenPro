using System;
using System.Linq;
using Microsoft.AspNetCore.Components.Authorization;
using HTLKrems.GradeManagement.Models;

namespace HTLKrems.GradeManagement.Services
{
    public interface IAuthService
    {
        Task<LoginResponse> LoginAsync(string email, string password);
        Task LogoutAsync();
        Task<User?> GetCurrentUserAsync();
    }

    public class AuthService : IAuthService
    {
        private readonly CustomAuthStateProvider _authProvider;
        private User? _currentUser;

        private readonly List<User> _users = new()
        {
            new User { Email = "student@htl.at", Name = "Max Mustermann", Role = nameof(UserRole.Student) },
            new User { Email = "teacher@htl.at", Name = "Anna Lehrer",    Role = nameof(UserRole.Teacher) },
            new User { Email = "admin@htl.at",   Name = "Admin Boss",     Role = nameof(UserRole.SchoolAdmin) }
        };

        public AuthService(AuthenticationStateProvider authProvider)
        {
            _authProvider = (CustomAuthStateProvider)authProvider;
        }

        public Task<User?> GetCurrentUserAsync() => Task.FromResult(_currentUser);

        public Task LogoutAsync()
        {
            _currentUser = null;
            _authProvider.MarkLoggedOut();
            return Task.CompletedTask;
        }

        public Task<LoginResponse> LoginAsync(string email, string password)
        {
            var found = _users.FirstOrDefault(u => string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));
            if (found is null)
                return Task.FromResult(new LoginResponse { Success = false, ErrorMessage = "User not found" });

            _currentUser = found;
            _authProvider.MarkAuthenticated(found.Name, found.Role.ToString()); // Enum → string

            // found ist hier garantiert nicht null → null-forgiving '!'
            return Task.FromResult(new LoginResponse { Success = true, User = found! });
        }

    }
}
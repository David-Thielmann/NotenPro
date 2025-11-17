using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
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

        // Demo-User, E-Mails passend zu QuickLogin
        private readonly List<User> _users = new()
        {
            new User { Email = "student@htl-krems.at",  Name = "Max Mustermann", Role = "Student" },
            new User { Email = "teacher@htl-krems.at",  Name = "Anna Lehrer",    Role = "Teacher" },
            new User { Email = "admin@htl-krems.at",    Name = "Admin Boss",     Role = "SchoolAdmin" },
            new User { Email = "sysadmin@htl-krems.at", Name = "Sysadmin",       Role = "SystemAdmin" },
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
            email = email?.Trim() ?? string.Empty;

            var found = _users.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));

            if (found is null)
            {
                return Task.FromResult(new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "User not found"
                });
            }

            // Demo: Passwort wird nicht wirklich geprüft.
            // Wenn du willst, kannst du hier z.B. password == "student"/"teacher"/... prüfen.

            _currentUser = found;

            // Role ist hier string (z.B. "Student", "Teacher", ...)
            _authProvider.MarkAuthenticated(found.Name, found.Role ?? string.Empty);

            return Task.FromResult(new LoginResponse
            {
                Success = true,
                User = found
            });
        }
    }
}

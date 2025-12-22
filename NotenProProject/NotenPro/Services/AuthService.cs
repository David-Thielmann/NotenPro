using System.Linq;
using System.Threading.Tasks;
using HTLKrems.GradeManagement.Models;
using Microsoft.AspNetCore.Components.Authorization;

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
        private readonly AuthStateProvider _authStateProvider;  // Verwende direkt den AuthStateProvider
        private User? _currentUser;

        // Demo users
        private readonly List<User> _demoUsers = new()
        {
            new User { Id = "1", Name = "Max Mustermann",  Email = "student@htl-krems.at", Role = UserRole.Student },
            new User { Id = "2", Name = "Prof. Müller",    Email = "teacher@htl-krems.at", Role = UserRole.Teacher },
            new User { Id = "3", Name = "Admin Schmidt",   Email = "admin@htl-krems.at",   Role = UserRole.SchoolAdmin },
            new User { Id = "4", Name = "System Admin",    Email = "sysadmin@htl-krems.at",Role = UserRole.SystemAdmin }
        };

        public AuthService(AuthStateProvider authStateProvider)  // Injiziere direkt den AuthStateProvider
        {
            _authStateProvider = authStateProvider;  // Keine Cast-Operation mehr nötig
        }

        public async Task<LoginResponse> LoginAsync(string email, string password)
        {
            // Simulierter API-Call
            await Task.Delay(300);

            var user = _demoUsers.FirstOrDefault(u =>
                string.Equals(u.Email, email, StringComparison.OrdinalIgnoreCase));

            if (user != null)
            {
                _currentUser = user;

                // AuthState aktualisieren
                _authStateProvider.MarkAuthenticated(
                    user.Name,
                    user.Email,
                    user.Role.ToString()
                );

                return new LoginResponse
                {
                    Success = true,
                    User = user
                };
            }

            return new LoginResponse
            {
                Success = false,
                ErrorMessage = "Ungültige Anmeldedaten"
            };
        }

        public Task<User?> GetCurrentUserAsync()
        {
            return Task.FromResult(_currentUser);
        }

        public Task LogoutAsync()
        {
            _currentUser = null;
            _authStateProvider.MarkLoggedOut();
            return Task.CompletedTask;
        }
    }
}

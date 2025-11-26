using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HTLKrems.GradeManagement.Services
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly ClaimsPrincipal _anonymous =
            new ClaimsPrincipal(new ClaimsIdentity());

        private ClaimsPrincipal _currentUser;

        public AuthStateProvider()
        {
            _currentUser = _anonymous;
        }

        public override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            return Task.FromResult(new AuthenticationState(_currentUser));
        }

        public void MarkAuthenticated(string name, string email, string role)
        {
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, role)
            }, "notenpro_auth");

            _currentUser = new ClaimsPrincipal(identity);

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(_currentUser)));
        }

        public void MarkLoggedOut()
        {
            _currentUser = _anonymous;

            NotifyAuthenticationStateChanged(
                Task.FromResult(new AuthenticationState(_currentUser)));
        }
    }
}
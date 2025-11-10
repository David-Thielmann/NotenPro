using System.Security.Claims;
using Microsoft.AspNetCore.Components.Authorization;

namespace HTLKrems.GradeManagement.Services;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _current = new(new ClaimsIdentity()); // anonym

    public override Task<AuthenticationState> GetAuthenticationStateAsync()
        => Task.FromResult(new AuthenticationState(_current));

    public void MarkAuthenticated(string name, string role = "Student")
    {
        var identity = new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, role),
        }, "FakeAuth");
        _current = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_current)));
    }

    public void MarkLoggedOut()
    {
        _current = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_current)));
    }
}
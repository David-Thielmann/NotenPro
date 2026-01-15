using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace HTLKrems.GradeManagement.Services;

/// <summary>
/// DEV only: Attaches the signed-in user's identity (oid, name, email, roles)
/// as HTTP headers so the API can resolve the user without an access token.
/// </summary>
public sealed class OidHeaderHandler : DelegatingHandler
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public OidHeaderHandler(AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user?.Identity?.IsAuthenticated == true)
        {
            string? GetClaim(string type) => user.FindFirst(type)?.Value;

            var oid =
                GetClaim("oid") ??
                GetClaim("http://schemas.microsoft.com/identity/claims/objectidentifier");

            if (!string.IsNullOrWhiteSpace(oid) && !request.Headers.Contains("X-User-Oid"))
                request.Headers.TryAddWithoutValidation("X-User-Oid", oid);

            var name = GetClaim("name") ?? user.Identity?.Name;
            if (!string.IsNullOrWhiteSpace(name) && !request.Headers.Contains("X-User-Name"))
                request.Headers.TryAddWithoutValidation("X-User-Name", name);

            var email =
                GetClaim("preferred_username") ??
                GetClaim("upn") ??
                GetClaim("email");

            if (!string.IsNullOrWhiteSpace(email) && !request.Headers.Contains("X-User-Email"))
                request.Headers.TryAddWithoutValidation("X-User-Email", email);

            foreach (var role in user.FindAll("roles"))
            {
                request.Headers.TryAddWithoutValidation("X-User-Role", role.Value);
            }
        }

        return await base.SendAsync(request, cancellationToken);
    }
}

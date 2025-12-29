namespace HTLKrems.GradeManagement.Services;

using System.Security.Claims;

public static class RoleHelper
{
    public static bool HasRole(ClaimsPrincipal user, string role)
    {
        return user.Claims
            .Where(c => c.Type == "roles")
            .SelectMany(c => c.Value.Trim('[', ']')
                .Replace("\"", "")
                .Split(',', StringSplitOptions.RemoveEmptyEntries))
            .Any(r => r.Trim() == role);
    }
}

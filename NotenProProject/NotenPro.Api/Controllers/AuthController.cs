using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;

namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly NotenProDbContext _dbContext;

    public AuthController(NotenProDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("me")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthMeDto>> Me()
    {
        var oid = User.FindFirstValue("oid")
                  ?? User.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");

        // DEV fallback: when no access token is available, the WASM client sends identity headers.
        if (string.IsNullOrWhiteSpace(oid) && Request.Headers.TryGetValue("X-User-Oid", out var oidHeader))
        {
            oid = oidHeader.ToString();
        }


        if (string.IsNullOrWhiteSpace(oid))
        {
            return BadRequest(new { error = "Missing oid claim" });
        }

        var email = User.FindFirstValue("preferred_username")
                    ?? User.FindFirstValue("upn")
                    ?? User.FindFirstValue("email")
                    ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) && Request.Headers.TryGetValue("X-User-Email", out var emailHeader))
        {
            email = emailHeader.ToString();
        }


        var name = User.FindFirstValue("name")
                   ?? User.Identity?.Name
                   ?? email
                   ?? "Unbekannter Benutzer";

        if (string.IsNullOrWhiteSpace(name) && Request.Headers.TryGetValue("X-User-Name", out var nameHeader))
        {
            name = nameHeader.ToString();
        }


        var roles = User.FindAll("roles").Select(c => c.Value).ToList();
        if (roles.Count == 0 && Request.Headers.TryGetValue("X-User-Role", out var roleHeaders))
        {
            roles = roleHeaders.ToArray().ToList();
        }

        var role = DetermineRoleFromClaims(roles);

        var user = await _dbContext.Users
            .Include(u => u.School)
            .FirstOrDefaultAsync(u => u.ExternalId == oid);

        if (user == null)
        {
            user = new UserEntity
            {
                ExternalId = oid,
                Email = email,
                Name = name,
                Role = role,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SchoolId = await GetDefaultSchoolIdAsync()
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            user.Email = string.IsNullOrWhiteSpace(email) ? user.Email : email;
            user.Name = string.IsNullOrWhiteSpace(name) ? user.Name : name;
            user.Role = role;
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        return Ok(new AuthMeDto
        {
            Id = user.Id,
            ExternalId = user.ExternalId,
            Name = user.Name,
            Email = user.Email,
            Role = user.Role.ToString(),
            SchoolId = user.SchoolId,
            SchoolName = user.School?.Name
        });
    }

    [HttpGet("ping")]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new
    {
        message = "API is running",
        auth = User.Identity?.IsAuthenticated ?? false,
        timestamp = DateTime.UtcNow
    });

    private static UserRole DetermineRoleFromClaims(List<string> roles)
    {
        if (roles == null || roles.Count == 0) return UserRole.Student;

        if (roles.Any(r => r.Contains("Admin", StringComparison.OrdinalIgnoreCase)))
            return UserRole.SchoolAdmin;
        if (roles.Any(r => r.Contains("Teacher", StringComparison.OrdinalIgnoreCase)))
            return UserRole.Teacher;

        return UserRole.Student;
    }

    private async Task<string?> GetDefaultSchoolIdAsync()
    {
        var school = await _dbContext.Schools.FirstOrDefaultAsync();
        if (school == null)
        {
            school = new SchoolEntity
            {
                Name = "HTL Krems",
                Location = "Krems",
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _dbContext.Schools.Add(school);
            await _dbContext.SaveChangesAsync();
        }
        return school.Id;
    }
}

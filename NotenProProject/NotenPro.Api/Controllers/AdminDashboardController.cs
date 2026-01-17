using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;

namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/admin/dashboard")]
public class AdminDashboardController : ControllerBase
{
    private readonly NotenProDbContext _dbContext;

    public AdminDashboardController(NotenProDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet("stats")]
    [AllowAnonymous] // DEV: WASM kann ohne AccessToken nur Identity-Header senden
    public async Task<ActionResult<AdminDashboardStatsDto>> GetStats()
    {
        var oid = User.FindFirstValue("oid")
                  ?? User.FindFirstValue("http://schemas.microsoft.com/identity/claims/objectidentifier");

        if (string.IsNullOrWhiteSpace(oid) && Request.Headers.TryGetValue("X-User-Oid", out var oidHeader))
            oid = oidHeader.ToString();

        if (string.IsNullOrWhiteSpace(oid))
            return BadRequest(new { error = "Missing oid claim" });

        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.ExternalId == oid);

        // If user is missing (fresh dev login), create it from headers so Admin endpoints work immediately.
        if (user == null)
        {
            var email = Request.Headers.TryGetValue("X-User-Email", out var emailHeader) ? emailHeader.ToString() : string.Empty;
            var name = Request.Headers.TryGetValue("X-User-Name", out var nameHeader) ? nameHeader.ToString() : string.Empty;

            user = new UserEntity
            {
                ExternalId = oid,
                Email = email,
                Name = name,
                Role = ResolveRoleFromHeaders(),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SchoolId = await GetDefaultSchoolIdAsync()
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
        }

        if (user.Role is not (UserRole.SchoolAdmin or UserRole.SystemAdmin))
            return Forbid();

        var schoolId = user.SchoolId;
        if (string.IsNullOrWhiteSpace(schoolId))
            return Ok(new AdminDashboardStatsDto());

        var teacherCount = await _dbContext.Users.AsNoTracking()
            .CountAsync(u => u.SchoolId == schoolId && u.Role == UserRole.Teacher);

        var studentCount = await _dbContext.Users.AsNoTracking()
            .CountAsync(u => u.SchoolId == schoolId && u.Role == UserRole.Student);

        var classCount = await _dbContext.Classes.AsNoTracking()
            .CountAsync(c => c.SchoolId == schoolId);

        var subjectCount = await _dbContext.Subjects.AsNoTracking()
            .CountAsync(s => s.SchoolId == schoolId);

        return Ok(new AdminDashboardStatsDto
        {
            TeacherCount = teacherCount,
            StudentCount = studentCount,
            ClassCount = classCount,
            SubjectCount = subjectCount
        });
    }

    private UserRole ResolveRoleFromHeaders()
    {
        // OidHeaderHandler sends one or multiple X-User-Role headers
        if (!Request.Headers.TryGetValue("X-User-Role", out var roles))
            return UserRole.Student;

        var roleValues = User.Claims
            .Where(c =>
                c.Type == ClaimTypes.Role ||
                c.Type == "roles" ||
                c.Type == "role")
            .Select(c => c.Value)
            .ToArray();

        var all = string.Join(" ", roleValues).ToLowerInvariant();


        if (all.Contains("admin")) return UserRole.SchoolAdmin;
        if (all.Contains("teacher")) return UserRole.Teacher;
        return UserRole.Student;
    }

    private async Task<string?> GetDefaultSchoolIdAsync()
    {
        var school = await _dbContext.Schools.AsNoTracking().FirstOrDefaultAsync();
        return school?.Id;
    }
}

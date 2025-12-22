using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Api.Data.Entities;

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
    [Authorize]
    public async Task<IActionResult> Me()
    {
        var oid = User.FindFirst("oid")?.Value;
        if (string.IsNullOrWhiteSpace(oid))
            return Unauthorized("Missing oid claim");

        var email =
            User.FindFirst("preferred_username")?.Value ??
            User.FindFirst("upn")?.Value ??
            User.FindFirst("email")?.Value ??
            string.Empty;

        var name = User.Identity?.Name ?? email;

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
                Role = UserRole.Student, // default (kannst du später mappen)
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                PasswordHash = null // falls du es optional machst
            };

            _dbContext.Users.Add(user);
        }
        else
        {
            user.Email = string.IsNullOrWhiteSpace(email) ? user.Email : email;
            user.Name = string.IsNullOrWhiteSpace(name) ? user.Name : name;
            user.UpdatedAt = DateTime.UtcNow;
        }

        await _dbContext.SaveChangesAsync();

        return Ok(new
        {
            user.Id,
            user.ExternalId,
            user.Name,
            user.Email,
            Role = user.Role.ToString(),
            user.SchoolId,
            SchoolName = user.School?.Name
        });
    }

    [HttpGet("ping")]
    [AllowAnonymous]
    public IActionResult Ping() => Ok("ok");
}

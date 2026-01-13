// Controllers/AuthController.cs
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    private readonly IConfiguration _configuration;

    public AuthController(NotenProDbContext dbContext, IConfiguration configuration)
    {
        _dbContext = dbContext;
        _configuration = configuration;
    }

    [HttpGet("me")]
    public async Task<IActionResult> Me()
    {
        try
        {
            Console.WriteLine($"=== /api/auth/me CALLED ===");
            Console.WriteLine($"Auth Header: {Request.Headers["Authorization"].FirstOrDefault()?.Substring(0, 50)}...");
            Console.WriteLine($"User Auth: {User.Identity?.IsAuthenticated}");
            
            // 🔥 METHODE 1: User aus Authentication Middleware
            if (User.Identity?.IsAuthenticated == true)
            {
                Console.WriteLine("✅ Using authenticated user from middleware");
                return await HandleAuthenticatedUser();
            }
            
            // 🔥 METHODE 2: Token aus Header manuell parsen
            var authHeader = Request.Headers["Authorization"].FirstOrDefault();
            if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer "))
            {
                var token = authHeader.Substring("Bearer ".Length).Trim();
                Console.WriteLine($"Token length: {token.Length}");
                
                try
                {
                    var handler = new JwtSecurityTokenHandler();
                    var jsonToken = handler.ReadJwtToken(token);
                    
                    var oid = jsonToken.Claims.FirstOrDefault(c => c.Type == "oid")?.Value;
                    var name = jsonToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;
                    var email = jsonToken.Claims.FirstOrDefault(c => c.Type == "preferred_username")?.Value;
                    var roles = jsonToken.Claims
                        .Where(c => c.Type == "roles")
                        .Select(c => c.Value)
                        .ToList();
                    
                    Console.WriteLine($"📋 Parsed from token - OID: {oid}, Name: {name}, Email: {email}");
                    
                    if (!string.IsNullOrEmpty(oid))
                    {
                        return await GetOrCreateUser(oid, name, email, roles);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Token parse error: {ex.Message}");
                }
            }
            
            // 🔥 METHODE 3: Mock-Daten als Fallback
            Console.WriteLine("⚠️ No valid auth, returning mock data");
            return await GetMockUser();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"❌ ERROR in /api/auth/me: {ex}");
            return StatusCode(500, new { error = ex.Message });
        }
    }

    private async Task<IActionResult> HandleAuthenticatedUser()
    {
        var oid = User.FindFirst("oid")?.Value 
                 ?? User.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier")?.Value;
        
        if (string.IsNullOrWhiteSpace(oid))
        {
            Console.WriteLine("❌ No OID in authenticated user");
            return BadRequest(new { error = "Missing oid claim" });
        }

        var email = User.FindFirst("preferred_username")?.Value 
                   ?? User.FindFirst("upn")?.Value 
                   ?? User.FindFirst("email")?.Value 
                   ?? string.Empty;

        var name = User.Identity?.Name 
                  ?? User.FindFirst("name")?.Value 
                  ?? email 
                  ?? "Unbekannter Benutzer";

        var roles = User.FindAll("roles").Select(c => c.Value).ToList();

        return await GetOrCreateUser(oid, name, email, roles);
    }

    private async Task<IActionResult> GetOrCreateUser(string oid, string name, string email, List<string> roles)
    {
        // User aus DB holen
        var user = await _dbContext.Users
            .Include(u => u.School)
            .FirstOrDefaultAsync(u => u.ExternalId == oid);

        if (user == null)
        {
            // Neuen User erstellen
            user = new UserEntity
            {
                ExternalId = oid,
                Email = email,
                Name = name,
                Role = DetermineRoleFromClaims(roles),
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                SchoolId = await GetDefaultSchoolIdAsync()
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();
            Console.WriteLine($"🆕 Created new user: {name} ({email})");
        }
        else
        {
            // User aktualisieren
            user.Email = string.IsNullOrWhiteSpace(email) ? user.Email : email;
            user.Name = string.IsNullOrWhiteSpace(name) ? user.Name : name;
            user.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();
        }

        return Ok(new
        {
            user.Id,
            user.ExternalId,
            user.Name,
            user.Email,
            Role = user.Role.ToString(),
            user.SchoolId,
            SchoolName = user.School?.Name ?? "HTL Krems",
            user.IsActive
        });
    }

    private async Task<IActionResult> GetMockUser()
    {
        // Mock User für Development
        var mockUser = new
        {
            Id = "mock-user-001",
            ExternalId = "mock-oid-123",
            Name = "Development User",
            Email = "dev@htl-krems.at",
            Role = "Teacher",
            SchoolId = "school-001",
            SchoolName = "HTL Krems",
            IsActive = true
        };
        
        return Ok(mockUser);
    }

    [HttpGet("ping")]
    [AllowAnonymous]
    public IActionResult Ping() => Ok(new { 
        message = "API is running", 
        auth = User.Identity?.IsAuthenticated ?? false,
        timestamp = DateTime.UtcNow 
    });

    private UserRole DetermineRoleFromClaims(List<string> roles)
    {
        if (roles == null) return UserRole.Student;
        
        if (roles.Any(r => r.Contains("Teacher", StringComparison.OrdinalIgnoreCase)))
            return UserRole.Teacher;
        if (roles.Any(r => r.Contains("Admin", StringComparison.OrdinalIgnoreCase)))
            return UserRole.SchoolAdmin;
        
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
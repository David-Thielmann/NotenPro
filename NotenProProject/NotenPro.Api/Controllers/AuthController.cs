using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Api.Data.Entities;
using NotenPro.Api.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly NotenProDbContext _dbContext;
    private readonly ILogger<AuthController> _logger;

    public AuthController(NotenProDbContext dbContext, ILogger<AuthController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login([FromBody] LoginRequest request)
    {
        try
        {
            var user = await _dbContext.Users
                .Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null)
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password"
                });
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Invalid email or password"
                });
            }

            if (!user.IsActive)
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Account is inactive"
                });
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                SchoolId = user.SchoolId,
                SchoolName = user.School?.Name,
                IsActive = user.IsActive
            };

            return Ok(new LoginResponse
            {
                Success = true,
                User = userDto,
                Token = "mock-jwt-token-" + user.Id // In production, generate real JWT
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login");
            return Ok(new LoginResponse
            {
                Success = false,
                ErrorMessage = "An error occurred during login"
            });
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult<LoginResponse>> Register([FromBody] RegisterRequest request)
    {
        try
        {
            // Check if email already exists
            if (await _dbContext.Users.AnyAsync(u => u.Email == request.Email))
            {
                return Ok(new LoginResponse
                {
                    Success = false,
                    ErrorMessage = "Email already registered"
                });
            }

            // Parse role
            if (!Enum.TryParse<UserRole>(request.Role, out var role))
            {
                return BadRequest("Invalid role");
            }

            // Create new user
            var user = new UserEntity
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = role,
                SchoolId = request.SchoolId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Users.Add(user);
            await _dbContext.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                SchoolId = user.SchoolId,
                IsActive = user.IsActive
            };

            return Ok(new LoginResponse
            {
                Success = true,
                User = userDto,
                Token = "mock-jwt-token-" + user.Id
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during registration");
            return Ok(new LoginResponse
            {
                Success = false,
                ErrorMessage = "An error occurred during registration"
            });
        }
    }

    [HttpGet("verify")]
    public async Task<ActionResult<LoginResponse>> VerifyToken([FromHeader(Name = "Authorization")] string? token)
    {
        try
        {
            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized();
            }

            // Extract user ID from token (in production, validate real JWT)
            var userId = token.Replace("Bearer ", "").Replace("mock-jwt-token-", "");

            var user = await _dbContext.Users
                .Include(u => u.School)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null || !user.IsActive)
            {
                return Unauthorized();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                SchoolId = user.SchoolId,
                SchoolName = user.School?.Name,
                IsActive = user.IsActive
            };

            return Ok(new LoginResponse
            {
                Success = true,
                User = userDto,
                Token = token.Replace("Bearer ", "")
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error verifying token");
            return Unauthorized();
        }
    }
}

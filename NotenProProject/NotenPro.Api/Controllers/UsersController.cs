using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Api.Data.Entities;
using NotenPro.Api.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<UsersController> _logger;

    public UsersController(NotenProDbContext context, ILogger<UsersController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserDto>>> GetAllUsers([FromQuery] string? role = null, [FromQuery] string? schoolId = null)
    {
        var query = _context.Users.Include(u => u.School).AsQueryable();

        if (!string.IsNullOrEmpty(role) && Enum.TryParse<UserRole>(role, out var userRole))
        {
            query = query.Where(u => u.Role == userRole);
        }

        if (!string.IsNullOrEmpty(schoolId))
        {
            query = query.Where(u => u.SchoolId == schoolId);
        }

        var users = await query
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString(),
                SchoolId = u.SchoolId,
                SchoolName = u.School != null ? u.School.Name : null,
                IsActive = u.IsActive
            })
            .OrderBy(u => u.Name)
            .ToListAsync();

        return Ok(users);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(string id)
    {
        var user = await _context.Users
            .Include(u => u.School)
            .Where(u => u.Id == id)
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString(),
                SchoolId = u.SchoolId,
                SchoolName = u.School != null ? u.School.Name : null,
                IsActive = u.IsActive
            })
            .FirstOrDefaultAsync();

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpGet("students")]
    public async Task<ActionResult<List<UserDto>>> GetAllStudents([FromQuery] string? schoolId = null, [FromQuery] string? classId = null)
    {
        var query = _context.Users
            .Include(u => u.School)
            .Where(u => u.Role == UserRole.Student)
            .AsQueryable();

        if (!string.IsNullOrEmpty(schoolId))
        {
            query = query.Where(u => u.SchoolId == schoolId);
        }

        if (!string.IsNullOrEmpty(classId))
        {
            query = query.Where(u => u.StudentClasses.Any(sc => sc.ClassId == classId));
        }

        var students = await query
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString(),
                SchoolId = u.SchoolId,
                SchoolName = u.School != null ? u.School.Name : null,
                IsActive = u.IsActive
            })
            .OrderBy(u => u.Name)
            .ToListAsync();

        return Ok(students);
    }

    [HttpGet("teachers")]
    public async Task<ActionResult<List<UserDto>>> GetAllTeachers([FromQuery] string? schoolId = null)
    {
        var query = _context.Users
            .Include(u => u.School)
            .Where(u => u.Role == UserRole.Teacher)
            .AsQueryable();

        if (!string.IsNullOrEmpty(schoolId))
        {
            query = query.Where(u => u.SchoolId == schoolId);
        }

        var teachers = await query
            .Select(u => new UserDto
            {
                Id = u.Id,
                Name = u.Name,
                Email = u.Email,
                Role = u.Role.ToString(),
                SchoolId = u.SchoolId,
                SchoolName = u.School != null ? u.School.Name : null,
                IsActive = u.IsActive
            })
            .OrderBy(u => u.Name)
            .ToListAsync();

        return Ok(teachers);
    }

    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            // Check if email already exists
            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
            {
                return BadRequest("A user with this email already exists");
            }

            // Parse role
            if (!Enum.TryParse<UserRole>(request.Role, out var role))
            {
                return BadRequest("Invalid role");
            }

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

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            var userDto = new UserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role.ToString(),
                SchoolId = user.SchoolId,
                IsActive = user.IsActive
            };

            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating user");
            return StatusCode(500, "Error creating user");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(string id, [FromBody] UpdateUserRequest request)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            // Check if new email conflicts with existing user
            if (await _context.Users.AnyAsync(u => u.Email == request.Email && u.Id != id))
            {
                return BadRequest("A user with this email already exists");
            }

            user.Name = request.Name;
            user.Email = request.Email;
            user.IsActive = request.IsActive;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user");
            return StatusCode(500, "Error updating user");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(string id)
    {
        try
        {
            var user = await _context.Users
                .Include(u => u.Grades)
                .Include(u => u.Notifications)
                .Include(u => u.StudentClasses)
                .Include(u => u.TeacherSubjects)
                .FirstOrDefaultAsync(u => u.Id == id);

            if (user == null)
                return NotFound();

            // Delete all related data
            _context.Grades.RemoveRange(user.Grades);
            _context.Notifications.RemoveRange(user.Notifications);
            _context.StudentClasses.RemoveRange(user.StudentClasses);
            _context.TeacherSubjects.RemoveRange(user.TeacherSubjects);

            // Delete tests if teacher
            var tests = await _context.Tests.Where(t => t.TeacherId == id).ToListAsync();
            foreach (var test in tests)
            {
                var grades = await _context.Grades.Where(g => g.TestId == test.Id).ToListAsync();
                _context.Grades.RemoveRange(grades);
            }
            _context.Tests.RemoveRange(tests);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user");
            return StatusCode(500, "Error deleting user");
        }
    }

    [HttpPut("{id}/password")]
    public async Task<ActionResult> UpdatePassword(string id, [FromBody] UpdatePasswordRequest request)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating password");
            return StatusCode(500, "Error updating password");
        }
    }

    [HttpGet("{id}/statistics")]
    public async Task<ActionResult<object>> GetUserStatistics(string id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return NotFound();

            if (user.Role == UserRole.Student)
            {
                var stats = new
                {
                    TotalGrades = await _context.Grades.CountAsync(g => g.StudentId == id && g.Status == GradeStatus.Graded),
                    AverageGrade = await _context.Grades
                        .Where(g => g.StudentId == id && g.GradeValue.HasValue && g.Status == GradeStatus.Graded)
                        .AverageAsync(g => (double?)g.GradeValue) ?? 0.0,
                    BestGrade = await _context.Grades
                        .Where(g => g.StudentId == id && g.GradeValue.HasValue && g.Status == GradeStatus.Graded)
                        .MinAsync(g => (decimal?)g.GradeValue) ?? 0.0m,
                    WorstGrade = await _context.Grades
                        .Where(g => g.StudentId == id && g.GradeValue.HasValue && g.Status == GradeStatus.Graded)
                        .MaxAsync(g => (decimal?)g.GradeValue) ?? 0.0m,
                    UnreadNotifications = await _context.Notifications.CountAsync(n => n.UserId == id && !n.IsRead)
                };
                return Ok(stats);
            }
            else if (user.Role == UserRole.Teacher)
            {
                var stats = new
                {
                    TotalTests = await _context.Tests.CountAsync(t => t.TeacherId == id),
                    TotalGradesGiven = await _context.Tests
                        .Where(t => t.TeacherId == id)
                        .SelectMany(t => t.Grades)
                        .CountAsync(g => g.Status == GradeStatus.Graded),
                    TotalSubjects = await _context.TeacherSubjects.CountAsync(ts => ts.TeacherId == id),
                    UnreadNotifications = await _context.Notifications.CountAsync(n => n.UserId == id && !n.IsRead)
                };
                return Ok(stats);
            }

            return Ok(new { });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting user statistics");
            return StatusCode(500, "Error getting user statistics");
        }
    }
}

public class UpdatePasswordRequest
{
    public string NewPassword { get; set; } = string.Empty;
}

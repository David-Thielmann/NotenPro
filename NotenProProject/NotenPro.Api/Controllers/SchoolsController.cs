using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SchoolsController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<SchoolsController> _logger;

    public SchoolsController(NotenProDbContext context, ILogger<SchoolsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<SchoolDto>>> GetAllSchools()
    {
        var schools = await _context.Schools
            .Include(s => s.Users)
            .Select(s => new SchoolDto
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                Status = s.Status,
                TeacherCount = s.Users.Count(u => u.Role == UserRole.Teacher && u.IsActive),
                StudentCount = s.Users.Count(u => u.Role == UserRole.Student && u.IsActive),
                CreatedAt = s.CreatedAt
            })
            .OrderBy(s => s.Name)
            .ToListAsync();

        return Ok(schools);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SchoolDto>> GetSchool(string id)
    {
        var school = await _context.Schools
            .Include(s => s.Users)
            .Where(s => s.Id == id)
            .Select(s => new SchoolDto
            {
                Id = s.Id,
                Name = s.Name,
                Location = s.Location,
                Status = s.Status,
                TeacherCount = s.Users.Count(u => u.Role == UserRole.Teacher && u.IsActive),
                StudentCount = s.Users.Count(u => u.Role == UserRole.Student && u.IsActive),
                CreatedAt = s.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (school == null)
            return NotFound();

        return Ok(school);
    }

    [HttpPost]
    public async Task<ActionResult<SchoolDto>> CreateSchool([FromBody] CreateSchoolRequest request)
    {
        try
        {
            // Check if school name already exists
            if (await _context.Schools.AnyAsync(s => s.Name == request.Name))
            {
                return BadRequest("A school with this name already exists");
            }

            var school = new SchoolEntity
            {
                Name = request.Name,
                Location = request.Location,
                Status = "Active",
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Schools.Add(school);
            await _context.SaveChangesAsync();

            var schoolDto = new SchoolDto
            {
                Id = school.Id,
                Name = school.Name,
                Location = school.Location,
                Status = school.Status,
                TeacherCount = 0,
                StudentCount = 0,
                CreatedAt = school.CreatedAt
            };

            return CreatedAtAction(nameof(GetSchool), new { id = school.Id }, schoolDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating school");
            return StatusCode(500, "Error creating school");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSchool(string id, [FromBody] UpdateSchoolRequest request)
    {
        try
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
                return NotFound();

            // Check if new name conflicts with existing school
            if (await _context.Schools.AnyAsync(s => s.Name == request.Name && s.Id != id))
            {
                return BadRequest("A school with this name already exists");
            }

            school.Name = request.Name;
            school.Location = request.Location;
            school.Status = request.Status;
            school.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating school");
            return StatusCode(500, "Error updating school");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSchool(string id)
    {
        try
        {
            var school = await _context.Schools
                .Include(s => s.Users)
                .Include(s => s.Classes)
                    .ThenInclude(c => c.Tests)
                        .ThenInclude(t => t.Grades)
                .Include(s => s.Subjects)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (school == null)
                return NotFound();

            // Check if school has any users
            if (school.Users.Any())
            {
                return BadRequest("Cannot delete school with existing users. Please remove all users first.");
            }

            // Delete all related data
            foreach (var classEntity in school.Classes)
            {
                foreach (var test in classEntity.Tests)
                {
                    _context.Grades.RemoveRange(test.Grades);
                }
                _context.Tests.RemoveRange(classEntity.Tests);
            }
            _context.Classes.RemoveRange(school.Classes);
            _context.Subjects.RemoveRange(school.Subjects);
            _context.Schools.Remove(school);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting school");
            return StatusCode(500, "Error deleting school");
        }
    }

    [HttpGet("{id}/statistics")]
    public async Task<ActionResult<object>> GetSchoolStatistics(string id)
    {
        try
        {
            var school = await _context.Schools.FindAsync(id);
            if (school == null)
                return NotFound();

            var stats = new
            {
                TotalStudents = await _context.Users.CountAsync(u => u.SchoolId == id && u.Role == UserRole.Student && u.IsActive),
                TotalTeachers = await _context.Users.CountAsync(u => u.SchoolId == id && u.Role == UserRole.Teacher && u.IsActive),
                TotalClasses = await _context.Classes.CountAsync(c => c.SchoolId == id),
                TotalSubjects = await _context.Subjects.CountAsync(s => s.SchoolId == id && s.IsActive),
                TotalTests = await _context.Tests.CountAsync(t => t.Class.SchoolId == id),
                TotalGrades = await _context.Grades.CountAsync(g => g.Student.SchoolId == id),
                AverageGrade = await _context.Grades
                    .Where(g => g.Student.SchoolId == id && g.GradeValue.HasValue && g.Status == GradeStatus.Graded)
                    .AverageAsync(g => (double?)g.GradeValue) ?? 0.0
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting school statistics");
            return StatusCode(500, "Error getting school statistics");
        }
    }
}

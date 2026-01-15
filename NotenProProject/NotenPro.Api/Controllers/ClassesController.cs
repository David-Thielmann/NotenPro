using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClassesController : ControllerBase
{
    private readonly NotenProDbContext _dbContext;
    private readonly ILogger<ClassesController> _logger;

    public ClassesController(NotenProDbContext dbContext, ILogger<ClassesController> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<ClassDto>>> GetAllClasses([FromQuery] string? schoolId = null)
    {
        var query = _dbContext.Classes
            .Include(c => c.ClassTeacher)
            .Include(c => c.StudentClasses)
                .ThenInclude(sc => sc.Student)
                    .ThenInclude(s => s.Grades)
            .AsQueryable();

        if (!string.IsNullOrEmpty(schoolId))
        {
            query = query.Where(c => c.SchoolId == schoolId);
        }

        var classes = await query
            .Select(c => new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                SchoolId = c.SchoolId,
                ClassTeacherId = c.ClassTeacherId,
                ClassTeacherName = c.ClassTeacher != null ? c.ClassTeacher.Name : null,
                StudentCount = c.StudentClasses.Count,
                AverageGrade = c.StudentClasses
                    .SelectMany(sc => sc.Student.Grades)
                    .Where(g => g.GradeValue.HasValue && g.Status == GradeStatus.Graded)
                    .Select(g => g.GradeValue!.Value)   // nach HasValue safe
                    .DefaultIfEmpty(0.0m)
                    .Average()

            })
            .OrderBy(c => c.Name)
            .ToListAsync();

        return Ok(classes);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ClassDto>> GetClass(string id)
    {
        var classEntity = await _dbContext.Classes
            .Include(c => c.ClassTeacher)
            .Include(c => c.StudentClasses)
                .ThenInclude(sc => sc.Student)
                    .ThenInclude(s => s.Grades)
            .Where(c => c.Id == id)
            .Select(c => new ClassDto
            {
                Id = c.Id,
                Name = c.Name,
                SchoolId = c.SchoolId,
                ClassTeacherId = c.ClassTeacherId,
                ClassTeacherName = c.ClassTeacher != null ? c.ClassTeacher.Name : null,
                StudentCount = c.StudentClasses.Count,
                AverageGrade = c.StudentClasses
                    .SelectMany(sc => sc.Student.Grades)
                    .Where(g => g.GradeValue.HasValue && g.Status == GradeStatus.Graded)
                    .Select(g => g.GradeValue!.Value)   // nach HasValue safe
                    .DefaultIfEmpty(0.0m)
                    .Average()

            })
            .FirstOrDefaultAsync();

        if (classEntity == null)
            return NotFound();

        return Ok(classEntity);
    }

    [HttpGet("{id}/students")]
    public async Task<ActionResult<List<UserDto>>> GetClassStudents(string id)
    {
        var students = await _dbContext.StudentClasses
            .Include(sc => sc.Student)
            .Where(sc => sc.ClassId == id)
            .Select(sc => new UserDto
            {
                Id = sc.Student.Id,
                Name = sc.Student.Name,
                Email = sc.Student.Email,
                Role = sc.Student.Role.ToString(),
                SchoolId = sc.Student.SchoolId,
                IsActive = sc.Student.IsActive
            })
            .OrderBy(s => s.Name)
            .ToListAsync();

        return Ok(students);
    }

    [HttpPost]
    public async Task<ActionResult<ClassDto>> CreateClass([FromBody] CreateClassRequest request)
    {
        try
        {
            // Check if class name already exists in school
            if (await _dbContext.Classes.AnyAsync(c => c.Name == request.Name && c.SchoolId == request.SchoolId))
            {
                return BadRequest("A class with this name already exists in this school");
            }

            var classEntity = new ClassEntity
            {
                Name = request.Name,
                SchoolId = request.SchoolId,
                ClassTeacherId = request.ClassTeacherId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _dbContext.Classes.Add(classEntity);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetClass), new { id = classEntity.Id }, classEntity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating class");
            return StatusCode(500, "Error creating class");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateClass(string id, [FromBody] UpdateClassRequest request)
    {
        try
        {
            var classEntity = await _dbContext.Classes.FindAsync(id);
            if (classEntity == null)
                return NotFound();

            // Check if new name conflicts with existing class
            if (await _dbContext.Classes.AnyAsync(c => c.Name == request.Name && c.SchoolId == classEntity.SchoolId && c.Id != id))
            {
                return BadRequest("A class with this name already exists in this school");
            }

            classEntity.Name = request.Name;
            classEntity.ClassTeacherId = request.ClassTeacherId;
            classEntity.UpdatedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating class");
            return StatusCode(500, "Error updating class");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClass(string id)
    {
        try
        {
            var classEntity = await _dbContext.Classes
                .Include(c => c.StudentClasses)
                .Include(c => c.Tests)
                    .ThenInclude(t => t.Grades)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (classEntity == null)
                return NotFound();

            // Delete all related data
            foreach (var test in classEntity.Tests)
            {
                _dbContext.Grades.RemoveRange(test.Grades);
            }
            _dbContext.Tests.RemoveRange(classEntity.Tests);
            _dbContext.StudentClasses.RemoveRange(classEntity.StudentClasses);
            _dbContext.Classes.Remove(classEntity);

            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting class");
            return StatusCode(500, "Error deleting class");
        }
    }

    [HttpPost("{classId}/students/{studentId}")]
    public async Task<ActionResult> AddStudentToClass(string classId, string studentId)
    {
        try
        {
            // Check if student is already in class
            if (await _dbContext.StudentClasses.AnyAsync(sc => sc.ClassId == classId && sc.StudentId == studentId))
            {
                return BadRequest("Student is already in this class");
            }

            var studentClass = new StudentClassEntity
            {
                ClassId = classId,
                StudentId = studentId,
                EnrolledAt = DateTime.UtcNow
            };

            _dbContext.StudentClasses.Add(studentClass);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding student to class");
            return StatusCode(500, "Error adding student to class");
        }
    }

    [HttpDelete("{classId}/students/{studentId}")]
    public async Task<ActionResult> RemoveStudentFromClass(string classId, string studentId)
    {
        try
        {
            var studentClass = await _dbContext.StudentClasses
                .FirstOrDefaultAsync(sc => sc.ClassId == classId && sc.StudentId == studentId);

            if (studentClass == null)
                return NotFound();

            _dbContext.StudentClasses.Remove(studentClass);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing student from class");
            return StatusCode(500, "Error removing student from class");
        }
    }
}

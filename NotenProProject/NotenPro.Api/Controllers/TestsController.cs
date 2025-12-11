using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Api.Data.Entities;
using NotenPro.Api.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestsController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<TestsController> _logger;

    public TestsController(NotenProDbContext context, ILogger<TestsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<TestDto>>> GetAllTests()
    {
        var tests = await _context.Tests
            .Include(t => t.Subject)
            .Include(t => t.Class)
            .Include(t => t.Teacher)
            .Include(t => t.Grades)
            .Select(t => new TestDto
            {
                Id = t.Id,
                Name = t.Name,
                SubjectId = t.SubjectId,
                Subject = t.Subject.Name,
                ClassId = t.ClassId,
                ClassName = t.Class.Name,
                TeacherId = t.TeacherId,
                TeacherName = t.Teacher.Name,
                Date = t.Date,
                MaxPoints = t.MaxPoints,
                Type = t.Type.ToString(),
                Description = t.Description,
                GradedCount = t.Grades.Count(g => g.Status == GradeStatus.Graded),
                TotalStudents = t.Class.StudentClasses.Count
            })
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        return Ok(tests);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<List<TestDto>>> GetTeacherTests(string teacherId)
    {
        var tests = await _context.Tests
            .Include(t => t.Subject)
            .Include(t => t.Class)
                .ThenInclude(c => c.StudentClasses)
            .Include(t => t.Teacher)
            .Include(t => t.Grades)
            .Where(t => t.TeacherId == teacherId)
            .Select(t => new TestDto
            {
                Id = t.Id,
                Name = t.Name,
                SubjectId = t.SubjectId,
                Subject = t.Subject.Name,
                ClassId = t.ClassId,
                ClassName = t.Class.Name,
                TeacherId = t.TeacherId,
                TeacherName = t.Teacher.Name,
                Date = t.Date,
                MaxPoints = t.MaxPoints,
                Type = t.Type.ToString(),
                Description = t.Description,
                GradedCount = t.Grades.Count(g => g.Status == GradeStatus.Graded),
                TotalStudents = t.Class.StudentClasses.Count
            })
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        return Ok(tests);
    }

    [HttpGet("class/{classId}")]
    public async Task<ActionResult<List<TestDto>>> GetClassTests(string classId)
    {
        var tests = await _context.Tests
            .Include(t => t.Subject)
            .Include(t => t.Class)
                .ThenInclude(c => c.StudentClasses)
            .Include(t => t.Teacher)
            .Include(t => t.Grades)
            .Where(t => t.ClassId == classId)
            .Select(t => new TestDto
            {
                Id = t.Id,
                Name = t.Name,
                SubjectId = t.SubjectId,
                Subject = t.Subject.Name,
                ClassId = t.ClassId,
                ClassName = t.Class.Name,
                TeacherId = t.TeacherId,
                TeacherName = t.Teacher.Name,
                Date = t.Date,
                MaxPoints = t.MaxPoints,
                Type = t.Type.ToString(),
                Description = t.Description,
                GradedCount = t.Grades.Count(g => g.Status == GradeStatus.Graded),
                TotalStudents = t.Class.StudentClasses.Count
            })
            .OrderByDescending(t => t.Date)
            .ToListAsync();

        return Ok(tests);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TestDto>> GetTest(string id)
    {
        var test = await _context.Tests
            .Include(t => t.Subject)
            .Include(t => t.Class)
                .ThenInclude(c => c.StudentClasses)
            .Include(t => t.Teacher)
            .Include(t => t.Grades)
            .Where(t => t.Id == id)
            .Select(t => new TestDto
            {
                Id = t.Id,
                Name = t.Name,
                SubjectId = t.SubjectId,
                Subject = t.Subject.Name,
                ClassId = t.ClassId,
                ClassName = t.Class.Name,
                TeacherId = t.TeacherId,
                TeacherName = t.Teacher.Name,
                Date = t.Date,
                MaxPoints = t.MaxPoints,
                Type = t.Type.ToString(),
                Description = t.Description,
                GradedCount = t.Grades.Count(g => g.Status == GradeStatus.Graded),
                TotalStudents = t.Class.StudentClasses.Count
            })
            .FirstOrDefaultAsync();

        if (test == null)
            return NotFound();

        return Ok(test);
    }

    [HttpPost]
    public async Task<ActionResult<TestDto>> CreateTest([FromBody] CreateTestRequest request, [FromQuery] string teacherId)
    {
        try
        {
            var test = new TestEntity
            {
                Name = request.Name,
                SubjectId = request.SubjectId,
                ClassId = request.ClassId,
                TeacherId = teacherId,
                Date = request.Date,
                MaxPoints = request.MaxPoints,
                Type = Enum.Parse<TestType>(request.Type),
                Description = request.Description,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            // Create pending grades for all students in the class
            var studentIds = await _context.StudentClasses
                .Where(sc => sc.ClassId == request.ClassId)
                .Select(sc => sc.StudentId)
                .ToListAsync();

            foreach (var studentId in studentIds)
            {
                var grade = new GradeEntity
                {
                    StudentId = studentId,
                    TestId = test.Id,
                    MaxPoints = request.MaxPoints,
                    Status = GradeStatus.Pending,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };
                _context.Grades.Add(grade);
            }

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTest), new { id = test.Id }, test);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating test");
            return StatusCode(500, "Error creating test");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateTest(string id, [FromBody] UpdateTestRequest request)
    {
        try
        {
            var test = await _context.Tests.FindAsync(id);
            if (test == null)
                return NotFound();

            test.Name = request.Name;
            test.Date = request.Date;
            test.MaxPoints = request.MaxPoints;
            test.Type = Enum.Parse<TestType>(request.Type);
            test.Description = request.Description;
            test.UpdatedAt = DateTime.UtcNow;

            // Update max points in all related grades
            var grades = await _context.Grades.Where(g => g.TestId == id).ToListAsync();
            foreach (var grade in grades)
            {
                grade.MaxPoints = request.MaxPoints;
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating test");
            return StatusCode(500, "Error updating test");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteTest(string id)
    {
        try
        {
            var test = await _context.Tests.Include(t => t.Grades).FirstOrDefaultAsync(t => t.Id == id);
            if (test == null)
                return NotFound();

            // Delete all related grades
            _context.Grades.RemoveRange(test.Grades);

            _context.Tests.Remove(test);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting test");
            return StatusCode(500, "Error deleting test");
        }
    }
}

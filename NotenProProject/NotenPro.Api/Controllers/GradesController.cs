using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Api.Data.Entities;
using NotenPro.Api.DTOs;
using HTLKrems.GradeManagement.Api.Services;
using NotenPro.Api.DTOs;

using HTLKrems.GradeManagement.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GradesController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<GradesController> _logger;
    private readonly IPdfExportService _pdfExport;

    public GradesController(
        NotenProDbContext context,
        ILogger<GradesController> logger,
        IPdfExportService pdfExport)
    {
        _context = context;
        _logger = logger;
        _pdfExport = pdfExport;
    }

    // ================== GET: api/grades ==================

    [HttpGet]
    public async Task<ActionResult<List<GradeDto>>> GetAllGrades()
    {
        var grades = await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Test)
                .ThenInclude(t => t.Subject)
            .Select(g => new GradeDto
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = g.Student.Name,
                TestId = g.TestId,
                TestName = g.Test.Name,
                Subject = g.Test.Subject.Name,
                GradeValue = g.GradeValue,
                Points = g.Points,
                MaxPoints = g.MaxPoints,
                Status = g.Status.ToString(),
                Comment = g.Comment,
                Date = g.Test.Date
            })
            .ToListAsync();

        return Ok(grades);
    }

    // ============ GET: api/grades/student/{studentId} ============

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<List<GradeDto>>> GetStudentGrades(string studentId)
    {
        var grades = await _context.Grades
            .Include(g => g.Test)
                .ThenInclude(t => t.Subject)
            .Include(g => g.Test)
                .ThenInclude(t => t.Teacher)
            .Include(g => g.Student)
            .Where(g => g.StudentId == studentId)
            .Select(g => new GradeDto
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = g.Student.Name,
                TestId = g.TestId,
                TestName = g.Test.Name,
                Subject = g.Test.Subject.Name,
                GradeValue = g.GradeValue,
                Points = g.Points,
                MaxPoints = g.MaxPoints,
                Status = g.Status.ToString(),
                Comment = g.Comment,
                Date = g.Test.Date
            })
            .OrderByDescending(g => g.Date)
            .ToListAsync();

        return Ok(grades);
    }

    // ====== NEU: GET: api/grades/student/{studentId}/export ======

    
    // ============ GET: api/grades/test/{testId} ============

    [HttpGet("test/{testId}")]
    public async Task<ActionResult<List<GradeDto>>> GetTestGrades(string testId)
    {
        var grades = await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Test)
                .ThenInclude(t => t.Subject)
            .Where(g => g.TestId == testId)
            .Select(g => new GradeDto
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = g.Student.Name,
                TestId = g.TestId,
                TestName = g.Test.Name,
                Subject = g.Test.Subject.Name,
                GradeValue = g.GradeValue,
                Points = g.Points,
                MaxPoints = g.MaxPoints,
                Status = g.Status.ToString(),
                Comment = g.Comment,
                Date = g.Test.Date
            })
            .OrderBy(g => g.StudentName)
            .ToListAsync();

        return Ok(grades);
    }

    // ============ GET: api/grades/{id} ============

    [HttpGet("{id}")]
    public async Task<ActionResult<GradeDto>> GetGrade(string id)
    {
        var grade = await _context.Grades
            .Include(g => g.Student)
            .Include(g => g.Test)
                .ThenInclude(t => t.Subject)
            .Where(g => g.Id == id)
            .Select(g => new GradeDto
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = g.Student.Name,
                TestId = g.TestId,
                TestName = g.Test.Name,
                Subject = g.Test.Subject.Name,
                GradeValue = g.GradeValue,
                Points = g.Points,
                MaxPoints = g.MaxPoints,
                Status = g.Status.ToString(),
                Comment = g.Comment,
                Date = g.Test.Date
            })
            .FirstOrDefaultAsync();

        if (grade == null)
            return NotFound();

        return Ok(grade);
    }

    // ============ POST: api/grades ============

    [HttpPost]
    public async Task<ActionResult<GradeDto>> CreateGrade([FromBody] CreateGradeRequest request)
    {
        try
        {
            // Check if grade already exists for this student and test
            if (await _context.Grades.AnyAsync(g => g.StudentId == request.StudentId && g.TestId == request.TestId))
            {
                return BadRequest("Grade already exists for this student and test");
            }

            // Get test to retrieve max points
            var test = await _context.Tests.FindAsync(request.TestId);
            if (test == null)
            {
                return BadRequest("Test not found");
            }

            var grade = new GradeEntity
            {
                StudentId = request.StudentId,
                TestId = request.TestId,
                GradeValue = request.GradeValue,
                Points = request.Points,
                MaxPoints = test.MaxPoints,
                Status = Enum.Parse<GradeStatus>(request.Status),
                Comment = request.Comment,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();

            // Create notification for student
            var student = await _context.Users.FindAsync(request.StudentId);
            if (student != null && grade.GradeValue.HasValue)
            {
                var notification = new NotificationEntity
                {
                    UserId = request.StudentId,
                    Title = "Neue Note verfügbar",
                    Message = $"Deine Note für '{test.Name}' wurde eingetragen: {grade.GradeValue:F2}",
                    Type = NotificationType.Success,
                    Timestamp = DateTime.UtcNow
                };
                _context.Notifications.Add(notification);
                await _context.SaveChangesAsync();
            }

            return CreatedAtAction(nameof(GetGrade), new { id = grade.Id }, grade);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating grade");
            return StatusCode(500, "Error creating grade");
        }
    }

    // ============ POST: api/grades/bulk ============

    [HttpPost("bulk")]
    public async Task<ActionResult> CreateBulkGrades([FromBody] BulkGradeRequest request)
    {
        try
        {
            var test = await _context.Tests
                .Include(t => t.Subject)
                .FirstOrDefaultAsync(t => t.Id == request.TestId);

            if (test == null)
            {
                return BadRequest("Test not found");
            }

            var notifications = new List<NotificationEntity>();

            foreach (var gradeInput in request.Grades)
            {
                // Check if grade already exists
                var existingGrade = await _context.Grades
                    .FirstOrDefaultAsync(g => g.StudentId == gradeInput.StudentId && g.TestId == request.TestId);

                if (existingGrade != null)
                {
                    // Update existing grade
                    existingGrade.GradeValue = gradeInput.GradeValue;
                    existingGrade.Points = gradeInput.Points;
                    existingGrade.Status = Enum.Parse<GradeStatus>(gradeInput.Status);
                    existingGrade.Comment = gradeInput.Comment;
                    existingGrade.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    // Create new grade
                    var grade = new GradeEntity
                    {
                        StudentId = gradeInput.StudentId,
                        TestId = request.TestId,
                        GradeValue = gradeInput.GradeValue,
                        Points = gradeInput.Points,
                        MaxPoints = test.MaxPoints,
                        Status = Enum.Parse<GradeStatus>(gradeInput.Status),
                        Comment = gradeInput.Comment,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow
                    };
                    _context.Grades.Add(grade);
                }

                // Create notification if graded
                if (gradeInput.GradeValue.HasValue && gradeInput.Status == "Graded")
                {
                    notifications.Add(new NotificationEntity
                    {
                        UserId = gradeInput.StudentId,
                        Title = "Neue Note verfügbar",
                        Message = $"Deine Note für '{test.Name}' ({test.Subject.Name}) wurde eingetragen: {gradeInput.GradeValue:F2}",
                        Type = NotificationType.Success,
                        Timestamp = DateTime.UtcNow
                    });
                }
            }

            await _context.SaveChangesAsync();

            // Add notifications
            if (notifications.Any())
            {
                _context.Notifications.AddRange(notifications);
                await _context.SaveChangesAsync();
            }

            return Ok(new { message = "Grades saved successfully" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating bulk grades");
            return StatusCode(500, "Error creating bulk grades");
        }
    }

    // ============ PUT: api/grades/{id} ============

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateGrade(string id, [FromBody] UpdateGradeRequest request)
    {
        try
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return NotFound();

            grade.GradeValue = request.GradeValue;
            grade.Points = request.Points;
            grade.Status = Enum.Parse<GradeStatus>(request.Status);
            grade.Comment = request.Comment;
            grade.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating grade");
            return StatusCode(500, "Error updating grade");
        }
    }

    // ============ DELETE: api/grades/{id} ============

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGrade(string id)
    {
        try
        {
            var grade = await _context.Grades.FindAsync(id);
            if (grade == null)
                return NotFound();

            _context.Grades.Remove(grade);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting grade");
            return StatusCode(500, "Error deleting grade");
        }
    }
    [HttpGet("export/student/{studentId}")]
    [Authorize] // falls du Auth verwendest
    public async Task<IActionResult> ExportStudentGrades(string studentId)
    {
        var grades = await _context.Grades
            .Include(g => g.Test).ThenInclude(t => t.Subject)
            .Include(g => g.Student)
            .Where(g => g.StudentId == studentId)
            .Select(g => new GradeDto
            {
                Id = g.Id,
                StudentId = g.StudentId,
                StudentName = g.Student.Name,
                TestId = g.TestId,
                TestName = g.Test.Name,
                Subject = g.Test.Subject.Name,
                GradeValue = g.GradeValue,
                Points = g.Points,
                MaxPoints = g.MaxPoints,
                Status = g.Status.ToString(),
                Comment = g.Comment,
                Date = g.Test.Date
            })
            .OrderByDescending(g => g.Date)
            .ToListAsync();

        if (!grades.Any())
            return BadRequest("Keine Noten zum Exportieren gefunden.");

        var pdfBytes = _pdfExport.CreateGradesPdf(grades);
        var fileName = $"Noten_{studentId}_{DateTime.Now:yyyyMMddHHmm}.pdf";

        return File(pdfBytes, "application/pdf", fileName);
    }

}



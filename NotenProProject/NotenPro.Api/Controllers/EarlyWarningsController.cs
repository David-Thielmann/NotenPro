using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Api.Data.Entities;
using NotenPro.Api.DTOs;

namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EarlyWarningsController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<EarlyWarningsController> _logger;

    public EarlyWarningsController(NotenProDbContext context, ILogger<EarlyWarningsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<EarlyWarningDto>>> GetAllEarlyWarnings()
    {
        var warnings = await _context.EarlyWarnings
            .Include(w => w.Student)
            .Include(w => w.Subject)
            .Include(w => w.Teacher)
            .Select(w => new EarlyWarningDto
            {
                Id = w.Id,
                StudentId = w.StudentId,
                StudentName = w.Student.Name,
                SubjectId = w.SubjectId,
                Subject = w.Subject.Name,
                TeacherId = w.TeacherId,
                TeacherName = w.Teacher.Name,
                Reason = w.Reason,
                CurrentAverage = w.CurrentAverage,
                IsSent = w.IsSent,
                SentAt = w.SentAt,
                CreatedAt = w.CreatedAt
            })
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();

        return Ok(warnings);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<List<EarlyWarningDto>>> GetTeacherEarlyWarnings(string teacherId)
    {
        var warnings = await _context.EarlyWarnings
            .Include(w => w.Student)
            .Include(w => w.Subject)
            .Include(w => w.Teacher)
            .Where(w => w.TeacherId == teacherId)
            .Select(w => new EarlyWarningDto
            {
                Id = w.Id,
                StudentId = w.StudentId,
                StudentName = w.Student.Name,
                SubjectId = w.SubjectId,
                Subject = w.Subject.Name,
                TeacherId = w.TeacherId,
                TeacherName = w.Teacher.Name,
                Reason = w.Reason,
                CurrentAverage = w.CurrentAverage,
                IsSent = w.IsSent,
                SentAt = w.SentAt,
                CreatedAt = w.CreatedAt
            })
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();

        return Ok(warnings);
    }

    [HttpGet("student/{studentId}")]
    public async Task<ActionResult<List<EarlyWarningDto>>> GetStudentEarlyWarnings(string studentId)
    {
        var warnings = await _context.EarlyWarnings
            .Include(w => w.Student)
            .Include(w => w.Subject)
            .Include(w => w.Teacher)
            .Where(w => w.StudentId == studentId)
            .Select(w => new EarlyWarningDto
            {
                Id = w.Id,
                StudentId = w.StudentId,
                StudentName = w.Student.Name,
                SubjectId = w.SubjectId,
                Subject = w.Subject.Name,
                TeacherId = w.TeacherId,
                TeacherName = w.Teacher.Name,
                Reason = w.Reason,
                CurrentAverage = w.CurrentAverage,
                IsSent = w.IsSent,
                SentAt = w.SentAt,
                CreatedAt = w.CreatedAt
            })
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();

        return Ok(warnings);
    }

    [HttpGet("pending")]
    public async Task<ActionResult<List<EarlyWarningDto>>> GetPendingEarlyWarnings([FromQuery] string? teacherId = null)
    {
        var query = _context.EarlyWarnings
            .Include(w => w.Student)
            .Include(w => w.Subject)
            .Include(w => w.Teacher)
            .Where(w => !w.IsSent)
            .AsQueryable();

        if (!string.IsNullOrEmpty(teacherId))
        {
            query = query.Where(w => w.TeacherId == teacherId);
        }

        var warnings = await query
            .Select(w => new EarlyWarningDto
            {
                Id = w.Id,
                StudentId = w.StudentId,
                StudentName = w.Student.Name,
                SubjectId = w.SubjectId,
                Subject = w.Subject.Name,
                TeacherId = w.TeacherId,
                TeacherName = w.Teacher.Name,
                Reason = w.Reason,
                CurrentAverage = w.CurrentAverage,
                IsSent = w.IsSent,
                SentAt = w.SentAt,
                CreatedAt = w.CreatedAt
            })
            .OrderByDescending(w => w.CreatedAt)
            .ToListAsync();

        return Ok(warnings);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EarlyWarningDto>> GetEarlyWarning(string id)
    {
        var warning = await _context.EarlyWarnings
            .Include(w => w.Student)
            .Include(w => w.Subject)
            .Include(w => w.Teacher)
            .Where(w => w.Id == id)
            .Select(w => new EarlyWarningDto
            {
                Id = w.Id,
                StudentId = w.StudentId,
                StudentName = w.Student.Name,
                SubjectId = w.SubjectId,
                Subject = w.Subject.Name,
                TeacherId = w.TeacherId,
                TeacherName = w.Teacher.Name,
                Reason = w.Reason,
                CurrentAverage = w.CurrentAverage,
                IsSent = w.IsSent,
                SentAt = w.SentAt,
                CreatedAt = w.CreatedAt
            })
            .FirstOrDefaultAsync();

        if (warning == null)
            return NotFound();

        return Ok(warning);
    }

    [HttpPost]
    public async Task<ActionResult<EarlyWarningDto>> CreateEarlyWarning([FromBody] CreateEarlyWarningRequest request, [FromQuery] string teacherId)
    {
        try
        {
            var warning = new EarlyWarningEntity
            {
                StudentId = request.StudentId,
                SubjectId = request.SubjectId,
                TeacherId = teacherId,
                Reason = request.Reason,
                CurrentAverage = request.CurrentAverage,
                IsSent = false,
                CreatedAt = DateTime.UtcNow
            };

            _context.EarlyWarnings.Add(warning);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetEarlyWarning), new { id = warning.Id }, warning);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating early warning");
            return StatusCode(500, "Error creating early warning");
        }
    }

    [HttpPost("send")]
    public async Task<ActionResult> SendEarlyWarnings([FromBody] SendEarlyWarningsRequest request)
    {
        try
        {
            var warnings = await _context.EarlyWarnings
                .Include(w => w.Student)
                .Include(w => w.Subject)
                .Include(w => w.Teacher)
                .Where(w => request.WarningIds.Contains(w.Id))
                .ToListAsync();

            if (!warnings.Any())
                return NotFound("No warnings found");

            var notifications = new List<NotificationEntity>();

            foreach (var warning in warnings)
            {
                warning.IsSent = true;
                warning.SentAt = DateTime.UtcNow;

                // Create notification for student
                notifications.Add(new NotificationEntity
                {
                    UserId = warning.StudentId,
                    Title = "Frühwarnung",
                    Message = $"Du hast eine Frühwarnung in {warning.Subject.Name} erhalten. Grund: {warning.Reason}. Aktueller Durchschnitt: {warning.CurrentAverage:F2}",
                    Type = NotificationType.Warning,
                    IsRead = false,
                    Timestamp = DateTime.UtcNow
                });
            }

            _context.Notifications.AddRange(notifications);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Sent {warnings.Count} early warnings" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending early warnings");
            return StatusCode(500, "Error sending early warnings");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEarlyWarning(string id)
    {
        try
        {
            var warning = await _context.EarlyWarnings.FindAsync(id);
            if (warning == null)
                return NotFound();

            _context.EarlyWarnings.Remove(warning);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting early warning");
            return StatusCode(500, "Error deleting early warning");
        }
    }

    [HttpGet("statistics/subject/{subjectId}")]
    public async Task<ActionResult<object>> GetSubjectWarningStatistics(string subjectId)
    {
        try
        {
            var stats = new
            {
                TotalWarnings = await _context.EarlyWarnings.CountAsync(w => w.SubjectId == subjectId),
                SentWarnings = await _context.EarlyWarnings.CountAsync(w => w.SubjectId == subjectId && w.IsSent),
                PendingWarnings = await _context.EarlyWarnings.CountAsync(w => w.SubjectId == subjectId && !w.IsSent),
                AverageGrade = await _context.EarlyWarnings
                    .Where(w => w.SubjectId == subjectId)
                    .AverageAsync(w => (double?)w.CurrentAverage) ?? 0.0
            };

            return Ok(stats);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting warning statistics");
            return StatusCode(500, "Error getting warning statistics");
        }
    }
}

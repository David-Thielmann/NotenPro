using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SubjectsController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<SubjectsController> _logger;

    public SubjectsController(NotenProDbContext context, ILogger<SubjectsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<SubjectDto>>> GetAllSubjects([FromQuery] string? schoolId = null)
    {
        var query = _context.Subjects.AsQueryable();

        if (!string.IsNullOrEmpty(schoolId))
        {
            query = query.Where(s => s.SchoolId == schoolId);
        }

        var subjects = await query
            .Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                SchoolId = s.SchoolId,
                IsActive = s.IsActive
            })
            .OrderBy(s => s.Name)
            .ToListAsync();

        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<SubjectDto>> GetSubject(string id)
    {
        var subject = await _context.Subjects
            .Where(s => s.Id == id)
            .Select(s => new SubjectDto
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                SchoolId = s.SchoolId,
                IsActive = s.IsActive
            })
            .FirstOrDefaultAsync();

        if (subject == null)
            return NotFound();

        return Ok(subject);
    }

    [HttpGet("teacher/{teacherId}")]
    public async Task<ActionResult<List<SubjectDto>>> GetTeacherSubjects(string teacherId)
    {
        var subjects = await _context.TeacherSubjects
            .Include(ts => ts.Subject)
            .Where(ts => ts.TeacherId == teacherId)
            .Select(ts => new SubjectDto
            {
                Id = ts.Subject.Id,
                Name = ts.Subject.Name,
                Description = ts.Subject.Description,
                SchoolId = ts.Subject.SchoolId,
                IsActive = ts.Subject.IsActive
            })
            .OrderBy(s => s.Name)
            .ToListAsync();

        return Ok(subjects);
    }

    [HttpPost]
    public async Task<ActionResult<SubjectDto>> CreateSubject([FromBody] CreateSubjectRequest request)
    {
        try
        {
            // Check if subject already exists in school
            if (await _context.Subjects.AnyAsync(s => s.Name == request.Name && s.SchoolId == request.SchoolId))
            {
                return BadRequest("A subject with this name already exists in this school");
            }

            var subject = new SubjectEntity
            {
                Name = request.Name,
                Description = request.Description,
                SchoolId = request.SchoolId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            _context.Subjects.Add(subject);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSubject), new { id = subject.Id }, subject);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating subject");
            return StatusCode(500, "Error creating subject");
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateSubject(string id, [FromBody] UpdateSubjectRequest request)
    {
        try
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
                return NotFound();

            // Check if new name conflicts with existing subject
            if (await _context.Subjects.AnyAsync(s => s.Name == request.Name && s.SchoolId == subject.SchoolId && s.Id != id))
            {
                return BadRequest("A subject with this name already exists in this school");
            }

            subject.Name = request.Name;
            subject.Description = request.Description;
            subject.IsActive = request.IsActive;
            subject.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating subject");
            return StatusCode(500, "Error updating subject");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteSubject(string id)
    {
        try
        {
            var subject = await _context.Subjects
                .Include(s => s.Tests)
                    .ThenInclude(t => t.Grades)
                .Include(s => s.TeacherSubjects)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (subject == null)
                return NotFound();

            // Delete all related data
            foreach (var test in subject.Tests)
            {
                _context.Grades.RemoveRange(test.Grades);
            }
            _context.Tests.RemoveRange(subject.Tests);
            _context.TeacherSubjects.RemoveRange(subject.TeacherSubjects);
            _context.Subjects.Remove(subject);

            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting subject");
            return StatusCode(500, "Error deleting subject");
        }
    }

    [HttpPost("{subjectId}/teachers/{teacherId}")]
    public async Task<ActionResult> AssignTeacherToSubject(string subjectId, string teacherId)
    {
        try
        {
            // Check if assignment already exists
            if (await _context.TeacherSubjects.AnyAsync(ts => ts.SubjectId == subjectId && ts.TeacherId == teacherId))
            {
                return BadRequest("Teacher is already assigned to this subject");
            }

            var teacherSubject = new TeacherSubjectEntity
            {
                SubjectId = subjectId,
                TeacherId = teacherId,
                AssignedAt = DateTime.UtcNow
            };

            _context.TeacherSubjects.Add(teacherSubject);
            await _context.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning teacher to subject");
            return StatusCode(500, "Error assigning teacher to subject");
        }
    }

    [HttpDelete("{subjectId}/teachers/{teacherId}")]
    public async Task<ActionResult> RemoveTeacherFromSubject(string subjectId, string teacherId)
    {
        try
        {
            var teacherSubject = await _context.TeacherSubjects
                .FirstOrDefaultAsync(ts => ts.SubjectId == subjectId && ts.TeacherId == teacherId);

            if (teacherSubject == null)
                return NotFound();

            _context.TeacherSubjects.Remove(teacherSubject);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing teacher from subject");
            return StatusCode(500, "Error removing teacher from subject");
        }
    }
}

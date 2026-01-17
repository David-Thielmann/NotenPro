using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;

namespace NotenPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeachersController : ControllerBase
    {
        private readonly NotenProDbContext _dbContext;

        public TeachersController(NotenProDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<ActionResult> CreateTeacher([FromBody] CreateTeacherRequest request)
        {
            // 1. Lehrer/User anlegen (wie bisher)
            var newUser = new UserEntity {
                Id = Guid.NewGuid().ToString(),
                Name = request.Teacher.Name,
                Email = request.Teacher.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
                Role = UserRole.Teacher,
                SchoolId = await _dbContext.Schools.Select(s => s.Id).FirstAsync(),
                IsActive = true
            };

            _dbContext.Users.Add(newUser);

            // 2. WENN ein Fach ausgewählt wurde, erstelle die Verknüpfung
            if (!string.IsNullOrEmpty(request.Teacher.PrimarySubjectId))
            {
                var teacherSubject = new TeacherSubjectEntity {
                    TeacherId = newUser.Id,
                    SubjectId = request.Teacher.PrimarySubjectId
                };
                _dbContext.TeacherSubjects.Add(teacherSubject);
            }

            await _dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult<List<TeacherDto>>> GetAllTeachers()
        {
            // Wir suchen alle User, deren Rolle 'Teacher' ist
            var teachers = await _dbContext.Users
                .AsNoTracking()
                .Where(u => u.Role == UserRole.Teacher) 
                .Select(u => new TeacherDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    // Holt das primäre Fach aus der TeacherSubjects Verknüpfung
                    SubjectName = u.TeacherSubjects.Select(ts => ts.Subject.Name).FirstOrDefault() ?? "Kein Fach"
                })
                .ToListAsync();

            return Ok(teachers);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(string id, [FromBody] TeacherDto teacherDto)
        {
            var teacher = await _dbContext.Users.FindAsync(id);
            if (teacher == null) return NotFound();

            // 1. Grunddaten aktualisieren
            teacher.Name = teacherDto.Name;
            teacher.Email = teacherDto.Email;
            teacher.IsActive = teacherDto.IsActive;

            // 2. Fach-Verknüpfung aktualisieren
            // Zuerst alte Verknüpfungen löschen
            var oldSubjects = _dbContext.TeacherSubjects.Where(ts => ts.TeacherId == id);
            _dbContext.TeacherSubjects.RemoveRange(oldSubjects);

            // Neue Verknüpfung hinzufügen, falls ein Fach gewählt wurde
            if (!string.IsNullOrEmpty(teacherDto.PrimarySubjectId))
            {
                _dbContext.TeacherSubjects.Add(new TeacherSubjectEntity
                {
                    TeacherId = id,
                    SubjectId = teacherDto.PrimarySubjectId
                });
            }

            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(string id)
        {
            var teacher = await _dbContext.Users.FindAsync(id);
            if (teacher == null) return NotFound();

            _dbContext.Users.Remove(teacher);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
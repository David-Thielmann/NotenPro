using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;

namespace NotenPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClassesController : ControllerBase
    {
        private readonly NotenProDbContext _dbContext;

        public ClassesController(NotenProDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<ClassDto>>> GetAllClasses()
        {
            try
            {
                // Wir holen die Daten erst in eine Liste, um LINQ-Konflikte zu vermeiden
                var classesList = await _dbContext.Classes.AsNoTracking().ToListAsync();
                var usersList = await _dbContext.Users.AsNoTracking().ToListAsync();

                var result = classesList.Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    SchoolId = c.SchoolId,
                    TeacherId = c.ClassTeacherId,
                    // Holt den Namen des Lehrers aus der Users-Liste
                    TeacherName = usersList
                        .FirstOrDefault(u => u.Id == c.ClassTeacherId)?.Name ?? "Nicht zugewiesen",
            
                    // WICHTIG: Prüfe in deiner UserEntity.cs ob es 'ClassId' oder 'S_ClassId' o.ä. heißt!
                    // Ich nutze hier eine sicherere Abfrage:
                    StudentCount = usersList.Count(u => 
                        u.Role.ToString() == "Student" && 
                        u.GetType().GetProperty("ClassId")?.GetValue(u)?.ToString() == c.Id),

                    AverageGrade = 0 // Kann später berechnet werden
                }).ToList();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Fehler: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult<ClassDto>> CreateClass([FromBody] ClassDto classDto)
        {
            if (classDto == null) return BadRequest("Daten sind leer");

            try
            {
                var newClass = new ClassEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = classDto.Name,
                    ClassTeacherId = classDto.TeacherId, // Mapping UI -> DB
                    SchoolId = (await _dbContext.Schools.AsNoTracking()
                                    .Select(s => s.Id)
                                    .FirstOrDefaultAsync())
                               ?? throw new InvalidOperationException("Keine School in der DB"),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _dbContext.Classes.Add(newClass);
                await _dbContext.SaveChangesAsync();

                classDto.Id = newClass.Id;
                return CreatedAtAction(nameof(GetAllClasses), new { id = newClass.Id }, classDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Serverfehler beim Erstellen: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(string id, [FromBody] ClassDto classDto)
        {
            if (id != classDto.Id) return BadRequest();

            var classEntity = await _dbContext.Classes.FindAsync(id);
            if (classEntity == null) return NotFound();

            classEntity.Name = classDto.Name;
            classEntity.ClassTeacherId = classDto.TeacherId; // Mapping UI -> DB
            classEntity.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(string id)
        {
            var classEntity = await _dbContext.Classes.FindAsync(id);
            if (classEntity == null) return NotFound();

            _dbContext.Classes.Remove(classEntity);
            await _dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;

namespace NotenPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly NotenProDbContext _dbContext;

        public SubjectsController(NotenProDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<SubjectDto>>> GetAllSubjects()
        {
            // Hinweis: .AsNoTracking() verbessert die Performance bei reinen Lesezugriffen
            var subjects = await _dbContext.Subjects
                .AsNoTracking()
                .Select(s => new SubjectDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    IsActive = s.IsActive
                })
                .ToListAsync();

            return Ok(subjects);
        }

        [HttpPost]
        public async Task<ActionResult<SubjectDto>> CreateSubject([FromBody] SubjectDto subjectDto)
        {
            if (subjectDto == null) return BadRequest("Daten sind leer");

            try 
            {
                // Wir erstellen die Entity aus dem DTO
                var newSubject = new SubjectEntity
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = subjectDto.Name,
                    Description = subjectDto.Description ?? "",
                    IsActive = true,
                    // WICHTIG: Hier muss eine existierende School-ID rein!
                    // Ich nehme hier die ID "e1..." als Platzhalter, 
                    // du solltest sie später dynamisch vom User/Admin laden.
                    SchoolId = "e1000000-0000-0000-0000-000000000005", 
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _dbContext.Subjects.Add(newSubject);
                await _dbContext.SaveChangesAsync();

                subjectDto.Id = newSubject.Id;
                return CreatedAtAction(nameof(GetAllSubjects), new { id = newSubject.Id }, subjectDto);
            }
            catch (Exception ex)
            {
                // Schau in deine Visual Studio Konsole (Ausgabe), hier steht der echte Fehler
                Console.WriteLine($"DB-FEHLER: {ex.Message}");
                if (ex.InnerException != null) Console.WriteLine($"Inner: {ex.InnerException.Message}");
        
                return StatusCode(500, $"Serverfehler: {ex.Message}");
            }
        } 
        
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(string id, [FromBody] SubjectDto subjectDto)
        {
            if (id != subjectDto.Id) return BadRequest();

            var subject = await _dbContext.Subjects.FindAsync(id);
            if (subject == null) return NotFound();

            subject.Name = subjectDto.Name;
            subject.Description = subjectDto.Description ?? "";
            subject.IsActive = subjectDto.IsActive;
            subject.UpdatedAt = DateTime.UtcNow;

            try
            {
                await _dbContext.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update Fehler: {ex.Message}");
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(string id)
        {
            var subject = await _dbContext.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            _dbContext.Subjects.Remove(subject);
            await _dbContext.SaveChangesAsync();
    
            return NoContent();
        }
    }
}
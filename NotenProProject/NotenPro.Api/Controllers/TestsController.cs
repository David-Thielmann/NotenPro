using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;
using System.Security.Claims;

namespace NotenPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TestsController : ControllerBase
    {
        private readonly NotenProDbContext _dbContext;

        public TestsController(NotenProDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Tests/my-tests
        [HttpGet("my-tests")]
        public async Task<ActionResult<List<TestDto>>> GetMyTests()
        {
            try
            {
                var tests = await _dbContext.Tests
                    .Include(t => t.Class)
                    .Include(t => t.Subject)
                    .Select(t => new TestDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        Date = t.Date,
                        // Konvertiert das Enum zum String für das DTO
                        Type = t.Type.ToString(), 
                        MaxPoints = t.MaxPoints,
                        ClassId = t.ClassId,
                        ClassName = t.Class != null ? t.Class.Name : "Unbekannt",
                        SubjectId = t.SubjectId,
                        SubjectName = t.Subject != null ? t.Subject.Name : "Unbekannt"
                    })
                    .ToListAsync();

                return Ok(tests);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fehler beim Laden der Tests: {ex.Message}");
                return StatusCode(500, "Interner Serverfehler beim Laden der Daten");
            }
        }

        [HttpPost]
        public async Task<ActionResult<TestDto>> CreateTest([FromBody] NotenPro.Shared.DTOs.CreateTestRequest request)
        {
            if (request == null) return BadRequest();

            try 
            {
                // 1. TeacherId holen (WICHTIG: Das Entity verlangt das!)
                // Wenn du ein Identity-System hast, nutzt man meist User.FindFirstValue
                var teacherId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        
                // Falls kein Teacher eingeloggt ist (Fallback für Tests, falls nötig)
                if (string.IsNullOrEmpty(teacherId))
                {
                    teacherId = await _dbContext.Users.Select(u => u.Id).FirstOrDefaultAsync();
                }

                // 2. ClassId Fallback (wie bisher)
                var finalClassId = request.ClassId;
                if (string.IsNullOrEmpty(finalClassId)) 
                {
                    finalClassId = await _dbContext.Classes.Select(c => c.Id).FirstOrDefaultAsync();
                }

                // 3. Entity erstellen
                var newTest = new TestEntity {
                    Id = Guid.NewGuid().ToString(),
                    Name = request.Name,
                    SubjectId = request.SubjectId,
                    ClassId = finalClassId!, 
                    TeacherId = teacherId!, // DIESE ZEILE HAT GEFEHLT!
                    Date = request.Date,
                    Type = Enum.TryParse<TestType>(request.Type, out var t) ? t : TestType.Test,
                    MaxPoints = request.MaxPoints,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                _dbContext.Tests.Add(newTest);
                await _dbContext.SaveChangesAsync();

                return Ok(new TestDto { Id = newTest.Id, Name = newTest.Name });
            }
            catch (Exception ex) 
            {
                // Logge den Fehler in die Konsole
                Console.WriteLine($"Fehler beim Erstellen des Tests: {ex.Message}");
                return StatusCode(500, $"Interner Fehler: {ex.Message}");
            }
        }

        // DELETE: api/Tests/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(string id)
        {
            try
            {
                var test = await _dbContext.Tests.FindAsync(id);
                if (test == null) return NotFound();

                _dbContext.Tests.Remove(test);
                await _dbContext.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Löschfehler: {ex.Message}");
                return StatusCode(500, "Fehler beim Löschen des Tests");
            }
        }
    }
}
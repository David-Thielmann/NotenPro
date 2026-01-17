using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;
using System.Security.Claims;
using NotenPro.Shared.DTOs;

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
            var tests = await _dbContext.Tests
                .Include(t => t.Class)
                .Include(t => t.Subject)
                .Select(t => new TestDto
                {
                    Id = t.Id,
                    Name = t.Name,
                    Date = t.Date,
                    // FEHLERBEHEBUNG: .ToString() konvertiert das Enum zum String für das DTO
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

// POST: api/Tests
        [HttpPost]
        public async Task<ActionResult<TestDto>> CreateTest([FromBody] CreateTestRequest request)
        {
            if (request == null) return BadRequest();

            var newTest = new TestEntity
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                ClassId = request.ClassId,
                SubjectId = request.SubjectId,
                Date = request.Date,
                // FEHLERBEHEBUNG: Wandelt den String vom Request zurück in das Backend-Enum um
                Type = Enum.Parse<TestType>(request.Type), 
                MaxPoints = request.MaxPoints,
                CreatedAt = DateTime.UtcNow
            };

            _dbContext.Tests.Add(newTest);
            await _dbContext.SaveChangesAsync();

            return Ok(new TestDto { Id = newTest.Id, Name = newTest.Name });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTest(string id)
        {
            var test = await _dbContext.Tests.FindAsync(id);
            if (test == null) return NotFound();

            _dbContext.Tests.Remove(test);
            await _dbContext.SaveChangesAsync();

            return NoContent();
        }
    }
}
// Controllers/StudentsController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;

namespace NotenPro.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly NotenProDbContext _dbContext;

        public StudentsController(NotenProDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet("{userId}/dashboard/stats")]
        public async Task<IActionResult> GetDashboardStats(string userId)
        {
            try
            {
                Console.WriteLine($"DEBUG: GetDashboardStats for user: {userId}");
                
                // 🔥 NUR DIESE TABELLE EXISTIERT IN DEINER DB:
                var user = await _dbContext.Users
                    .FirstOrDefaultAsync(u => u.Id == userId);
                
                if (user == null)
                {
                    return NotFound(new { error = $"User {userId} nicht gefunden" });
                }

                // 🔥 KEINE GradeEntity, TestEntity, NotificationEntity in deiner DB!
                // Deshalb: Leere/Default Werte zurückgeben
                
                decimal averageGrade = 0.0m;      // Keine Grades Tabelle
                int ungradedTests = 0;           // Keine Tests Tabelle  
                int unreadNotifications = 0;     // Keine Notifications Tabelle
                string className = "";           // User hat kein ClassId/ClassName

                // 🔥 OPTIONAL: Du könntest später die Properties zu UserEntity hinzufügen:
                // - AverageGrade (als Property in UserEntity)
                // - ClassName (als Property in UserEntity)
                // - etc.

                return Ok(new
                {
                    AverageGrade = averageGrade,
                    UngradedTests = ungradedTests,
                    UnreadNotifications = unreadNotifications,
                    ClassName = className
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ERROR in GetDashboardStats: {ex}");
                return StatusCode(500, new { error = "Interner Serverfehler", details = ex.Message });
            }
        }
    }
}
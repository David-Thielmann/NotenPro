using HTLKrems.GradeManagement.Models;

namespace HTLKrems.GradeManagement.Services
{
    public interface IGradeService
    {
        Task<List<Grade>> GetMyGradesAsync();
        Task<List<Grade>> GetRecentGradesAsync(int count);
        Task<List<SubjectAverage>> GetSubjectAveragesAsync();
        Task<ApiResponse<bool>> SaveGradesAsync(string testId, List<StudentGradeEntry> grades);
    }

    public class GradeService : IGradeService
    {
        private readonly List<Grade> _mockGrades = new()
        {
            new Grade 
            { 
                Id = "1", 
                Subject = "Mathematik", 
                TestName = "Test 1", 
                GradeValue = 2.0m,
                Points = 85,
                MaxPoints = 100,
                Date = DateTime.Now.AddDays(-7),
                TeacherName = "Prof. Müller"
            },
            new Grade 
            { 
                Id = "2", 
                Subject = "Deutsch", 
                TestName = "Schularbeit", 
                GradeValue = 1.0m,
                Points = 95,
                MaxPoints = 100,
                Date = DateTime.Now.AddDays(-14),
                TeacherName = "Prof. Schmidt"
            },
            new Grade 
            { 
                Id = "3", 
                Subject = "Programmieren", 
                TestName = "Projekt", 
                GradeValue = 1.5m,
                Points = 90,
                MaxPoints = 100,
                Date = DateTime.Now.AddDays(-21),
                TeacherName = "Prof. Wagner"
            },
            new Grade 
            { 
                Id = "4", 
                Subject = "Englisch", 
                TestName = "Test 2", 
                GradeValue = 2.5m,
                Points = 75,
                MaxPoints = 100,
                Date = DateTime.Now.AddDays(-28),
                TeacherName = "Prof. Fischer"
            },
            new Grade 
            { 
                Id = "5", 
                Subject = "Mathematik", 
                TestName = "Test 2", 
                GradeValue = 1.0m,
                Points = 98,
                MaxPoints = 100,
                Date = DateTime.Now.AddDays(-35),
                TeacherName = "Prof. Müller"
            }
        };

        public Task<List<Grade>> GetMyGradesAsync()
        {
            return Task.FromResult(_mockGrades.OrderByDescending(g => g.Date).ToList());
        }

        public Task<List<Grade>> GetRecentGradesAsync(int count)
        {
            return Task.FromResult(_mockGrades.OrderByDescending(g => g.Date).Take(count).ToList());
        }

        public Task<List<SubjectAverage>> GetSubjectAveragesAsync()
        {
            var averages = _mockGrades
                .GroupBy(g => g.Subject)
                .Select(g => new SubjectAverage
                {
                    Name = g.Key,
                    Average = g.Average(x => x.GradeValue),
                    TestCount = g.Count()
                })
                .ToList();

            return Task.FromResult(averages);
        }

        public async Task<ApiResponse<bool>> SaveGradesAsync(string testId, List<StudentGradeEntry> grades)
        {
            await Task.Delay(500); // Simulate API call
            return new ApiResponse<bool> { Success = true, Data = true };
        }
    }

    public class SubjectAverage
    {
        public string Name { get; set; } = "";
        public decimal Average { get; set; }
        public int TestCount { get; set; }
    }
}

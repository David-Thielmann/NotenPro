using HTLKrems.GradeManagement.Models;

namespace HTLKrems.GradeManagement.Services
{
    // ==================== NOTIFICATION SERVICE ====================
    public interface INotificationService
    {
        Task<List<Notification>> GetMyNotificationsAsync();
        Task<int> GetUnreadCountAsync();
        Task<bool> MarkAsReadAsync(string id);
    }

    public class NotificationService : INotificationService
    {
        private readonly List<Notification> _mockNotifications = new()
        {
            new Notification { Id = "1", Title = "Neue Note", Message = "Mathematik Test 1 wurde benotet", Type = NotificationType.Success, Timestamp = DateTime.Now.AddHours(-2), IsRead = false },
            new Notification { Id = "2", Title = "Test angekündigt", Message = "Deutscher Test nächste Woche", Type = NotificationType.Info, Timestamp = DateTime.Now.AddHours(-5), IsRead = false },
            new Notification { Id = "3", Title = "Frühwarnung", Message = "Englisch: Verbesserung notwendig", Type = NotificationType.Warning, Timestamp = DateTime.Now.AddDays(-1), IsRead = true }
        };

        public Task<List<Notification>> GetMyNotificationsAsync() => Task.FromResult(_mockNotifications);
        public Task<int> GetUnreadCountAsync() => Task.FromResult(_mockNotifications.Count(n => !n.IsRead));
        
        public Task<bool> MarkAsReadAsync(string id)
        {
            var notification = _mockNotifications.FirstOrDefault(n => n.Id == id);
            if (notification != null) notification.IsRead = true;
            return Task.FromResult(true);
        }
    }

    // ==================== STUDENT SERVICE ====================
    public interface IStudentService
    {
        Task<StudentDashboardStats> GetDashboardStatsAsync();
    }

    public class StudentService : IStudentService
    {
        public Task<StudentDashboardStats> GetDashboardStatsAsync()
        {
            return Task.FromResult(new StudentDashboardStats
            {
                AverageGrade = 2.1m,
                UngradedTests = 3,
                UnreadNotifications = 5,
                ClassName = "3AHIF"
            });
        }
    }

    public class StudentDashboardStats
    {
        public decimal AverageGrade { get; set; }
        public int UngradedTests { get; set; }
        public int UnreadNotifications { get; set; }
        public string ClassName { get; set; } = "";
    }

    // ==================== TEST SERVICE ====================
    public interface ITestService
    {
        Task<List<Test>> GetMyTestsAsync();
        Task<Test?> GetTestByIdAsync(string id);
        Task<ApiResponse<Test>> CreateTestAsync(CreateTestRequest request);
        Task<bool> DeleteTestAsync(string id);
    }

    public class TestService : ITestService
    {
        private List<Test> _mockTests = new()
        {
            new Test { Id = "1", Name = "Test 1", Subject = "Mathematik", Date = DateTime.Now.AddDays(7), ClassName = "3AHIF", MaxPoints = 100, Type = TestType.Test },
            new Test { Id = "2", Name = "Schularbeit", Subject = "Deutsch", Date = DateTime.Now.AddDays(14), ClassName = "4AHIF", MaxPoints = 100, Type = TestType.Schularbeit }
        };

        public Task<List<Test>> GetMyTestsAsync() => Task.FromResult(_mockTests);
        public Task<Test?> GetTestByIdAsync(string id) => Task.FromResult(_mockTests.FirstOrDefault(t => t.Id == id));
        
        public async Task<ApiResponse<Test>> CreateTestAsync(CreateTestRequest request)
        {
            await Task.Delay(300);
            var test = new Test
            {
                Id = Guid.NewGuid().ToString(),
                Name = request.Name,
                SubjectId = request.SubjectId,
                Date = request.Date,
                ClassId = request.ClassId,
                MaxPoints = request.MaxPoints,
                Type = request.Type
            };
            _mockTests.Add(test);
            return new ApiResponse<Test> { Success = true, Data = test };
        }

        public Task<bool> DeleteTestAsync(string id)
        {
            _mockTests.RemoveAll(t => t.Id == id);
            return Task.FromResult(true);
        }
    }

    // ==================== TEACHER SERVICE ====================
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<ApiResponse<Teacher>> CreateTeacherAsync(Teacher teacher);
        Task<ApiResponse<bool>> DeleteTeacherAsync(string id);
    }

    public class TeacherService : ITeacherService
    {
        private List<Teacher> _mockTeachers = new()
        {
            new Teacher { Id = "1", Name = "Prof. Müller", Email = "mueller@htl-krems.at", Subjects = new() { "Mathematik" }, IsActive = true },
            new Teacher { Id = "2", Name = "Prof. Schmidt", Email = "schmidt@htl-krems.at", Subjects = new() { "Deutsch" }, IsActive = true }
        };

        public Task<List<Teacher>> GetAllTeachersAsync() => Task.FromResult(_mockTeachers);
        
        public async Task<ApiResponse<Teacher>> CreateTeacherAsync(Teacher teacher)
        {
            await Task.Delay(300);
            teacher.Id = Guid.NewGuid().ToString();
            _mockTeachers.Add(teacher);
            return new ApiResponse<Teacher> { Success = true, Data = teacher };
        }

        public Task<ApiResponse<bool>> DeleteTeacherAsync(string id)
        {
            _mockTeachers.RemoveAll(t => t.Id == id);
            return Task.FromResult(new ApiResponse<bool> { Success = true, Data = true });
        }
    }

    // ==================== CLASS SERVICE ====================
    public interface IClassService
    {
        Task<List<Class>> GetAllClassesAsync();
        Task<List<Class>> GetMyClassesAsync();
        Task<ApiResponse<Class>> CreateClassAsync(Class cls);
    }

    public class ClassService : IClassService
    {
        private List<Class> _mockClasses = new()
        {
            new Class { Id = "1", Name = "3AHIF", ClassTeacherName = "Prof. Müller", StudentCount = 28, AverageGrade = 2.3m },
            new Class { Id = "2", Name = "4AHIF", ClassTeacherName = "Prof. Schmidt", StudentCount = 26, AverageGrade = 2.1m }
        };

        public Task<List<Class>> GetAllClassesAsync() => Task.FromResult(_mockClasses);
        public Task<List<Class>> GetMyClassesAsync() => Task.FromResult(_mockClasses);
        
        public async Task<ApiResponse<Class>> CreateClassAsync(Class cls)
        {
            await Task.Delay(300);
            cls.Id = Guid.NewGuid().ToString();
            _mockClasses.Add(cls);
            return new ApiResponse<Class> { Success = true, Data = cls };
        }
    }

    // ==================== SUBJECT SERVICE ====================
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjectsAsync();
        Task<List<Subject>> GetMySubjectsAsync();
        Task<ApiResponse<Subject>> CreateSubjectAsync(Subject subject);
        Task<ApiResponse<bool>> DeleteSubjectAsync(string id);
    }

    public class SubjectService : ISubjectService
    {
        private List<Subject> _mockSubjects = new()
        {
            new Subject { Id = "1", Name = "Mathematik", Description = "Höhere Mathematik", IsActive = true },
            new Subject { Id = "2", Name = "Deutsch", Description = "Deutsche Sprache", IsActive = true },
            new Subject { Id = "3", Name = "Programmieren", Description = "Softwareentwicklung", IsActive = true }
        };

        public Task<List<Subject>> GetAllSubjectsAsync() => Task.FromResult(_mockSubjects);
        public Task<List<Subject>> GetMySubjectsAsync() => Task.FromResult(_mockSubjects);
        
        public async Task<ApiResponse<Subject>> CreateSubjectAsync(Subject subject)
        {
            await Task.Delay(300);
            subject.Id = Guid.NewGuid().ToString();
            _mockSubjects.Add(subject);
            return new ApiResponse<Subject> { Success = true, Data = subject };
        }

        public Task<ApiResponse<bool>> DeleteSubjectAsync(string id)
        {
            _mockSubjects.RemoveAll(s => s.Id == id);
            return Task.FromResult(new ApiResponse<bool> { Success = true, Data = true });
        }
    }

    // ==================== SCHOOL SERVICE ====================
    public interface ISchoolService
    {
        Task<List<School>> GetAllSchoolsAsync();
        Task<ApiResponse<School>> CreateSchoolAsync(School school);
    }

    public class SchoolService : ISchoolService
    {
        private List<School> _mockSchools = new()
        {
            new School { Id = "1", Name = "HTL Krems", Location = "Krems", TeacherCount = 45, StudentCount = 520, Status = "Active" },
            new School { Id = "2", Name = "HTL St. Pölten", Location = "St. Pölten", TeacherCount = 38, StudentCount = 450, Status = "Active" }
        };

        public Task<List<School>> GetAllSchoolsAsync() => Task.FromResult(_mockSchools);
        
        public async Task<ApiResponse<School>> CreateSchoolAsync(School school)
        {
            await Task.Delay(300);
            school.Id = Guid.NewGuid().ToString();
            _mockSchools.Add(school);
            return new ApiResponse<School> { Success = true, Data = school };
        }
    }
}

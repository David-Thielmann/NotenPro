// HTL Krems Notenverwaltung - Datenmodelle
namespace HTLKrems.GradeManagement.Models
{
    // ==================== ENUMS ====================
    
    public enum UserRole
    {
        Student,
        Teacher,
        SchoolAdmin,
        SystemAdmin
    }

    public enum TestType
    {
        Test,
        Schularbeit,
        Mitarbeit,
        Hausübung
    }

    public enum GradeStatus
    {
        Graded,
        Pending,
        Absent
    }

    public enum NotificationType
    {
        Info,
        Warning,
        Success,
        Error
    }

    // ==================== USER MODELS ====================

    public partial class User
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = nameof(UserRole.Student);
        public string? SchoolId { get; set; }
        public bool IsActive { get; set; } = true;
    }

    // ==================== STUDENT MODELS ====================

    public class Grade
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; } = string.Empty;
        public string TestId { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string TestName { get; set; } = string.Empty;
        public decimal GradeValue { get; set; }
        public int? Points { get; set; }
        public int? MaxPoints { get; set; }
        public DateTime Date { get; set; }
        public string TeacherId { get; set; } = string.Empty;
        public string TeacherName { get; set; } = string.Empty;
        public GradeStatus Status { get; set; } = GradeStatus.Graded;
        public string? Comment { get; set; }
    }

    public class Notification
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public NotificationType Type { get; set; } = NotificationType.Info;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public bool IsRead { get; set; } = false;
    }

    // ==================== TEACHER MODELS ====================

    public class Teacher
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<string> Subjects { get; set; } = new();
        public bool IsActive { get; set; } = true;
    }

    public class Test
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string SubjectId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string TeacherId { get; set; } = string.Empty;
        public int MaxPoints { get; set; }
        public TestType Type { get; set; }
        public string? Description { get; set; }
    }

    public class StudentGradeEntry
    {
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public decimal? GradeValue { get; set; }
        public int? Points { get; set; }
        public GradeStatus Status { get; set; } = GradeStatus.Pending;
        public string? Comment { get; set; }
    }

    public class EarlyWarning
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string StudentId { get; set; } = string.Empty;
        public string StudentName { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public bool IsSent { get; set; } = false;
        public decimal CurrentAverage { get; set; }
    }

    // ==================== CLASS MODELS ====================

    public class Class
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string ClassTeacherId { get; set; } = string.Empty;
        public string ClassTeacherName { get; set; } = string.Empty;
        public int StudentCount { get; set; }
        public decimal AverageGrade { get; set; }
    }

    // ==================== SUBJECT MODELS ====================

    public class Subject
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }

    // ==================== SCHOOL MODELS ====================

    public class School
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public int TeacherCount { get; set; }
        public int StudentCount { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }

    // ==================== DTOs ====================

    public class LoginRequest
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class LoginResponse
    {
        public bool Success { get; set; }
        public HTLKrems.GradeManagement.Models.User? User { get; set; }
        public string? ErrorMessage { get; set; }
    }

    public class CreateTestRequest
    {
        public string Name { get; set; } = string.Empty;
        public string SubjectId { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string ClassId { get; set; } = string.Empty;
        public int MaxPoints { get; set; }
        public TestType Type { get; set; }
        public string? Description { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
    }
}

using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;
using CreateTestRequest = HTLKrems.GradeManagement.Models.CreateTestRequest;

namespace HTLKrems.GradeManagement.Services
{
    // ==================== NOTIFICATION SERVICE ====================
    public interface INotificationService
    {
        Task<List<Notification>> GetMyNotificationsAsync();
        Task<int> GetUnreadCountAsync();
        Task<bool> MarkAsReadAsync(string id);
    }

    // ==================== STUDENT SERVICE ====================
    public interface IStudentService
    {
        Task<StudentDashboardStats> GetDashboardStatsAsync();
        Task<StudentProfileDto> GetMyProfileAsync(); // neu für "Klasse"
    }

    // ==================== TEST SERVICE ====================
    public interface ITestService
    {
        Task<List<Test>> GetMyTestsAsync();
        Task<Test?> GetTestByIdAsync(string id);
        Task<ApiResponse<Test>> CreateTestAsync(CreateTestRequest request);
        Task<ApiResponse<bool>> DeleteTestAsync(string id);
    }

    // ==================== TEACHER SERVICE ====================
    /*
    public interface ITeacherService
    {
        Task<List<Teacher>> GetAllTeachersAsync();
        Task<ApiResponse<Teacher>> CreateTeacherAsync(Teacher teacher);
        Task<ApiResponse<bool>> DeleteTeacherAsync(string id);
        Task<List<Teacher>> GetTeachersAsync();
    }
*/
    // ==================== CLASS SERVICE ====================
    public interface IClassService
    {
        Task<List<Class>> GetAllClassesAsync();
        Task<List<Class>> GetMyClassesAsync();
        Task<ApiResponse<Class>> CreateClassAsync(Class cls);
        Task<List<Class>> GetClassesAsync();
    }

    // ==================== SUBJECT SERVICE ====================
    public interface ISubjectService
    {
        Task<List<Subject>> GetAllSubjectsAsync();
        Task<List<Subject>> GetMySubjectsAsync();
        Task<ApiResponse<Subject>> CreateSubjectAsync(Subject subject);
        Task<ApiResponse<bool>> DeleteSubjectAsync(string id);
        Task<List<Subject>> GetSubjectsAsync();
    }

    // ==================== SCHOOL SERVICE ====================
    public interface ISchoolService
    {
        Task<List<School>> GetAllSchoolsAsync();
        Task<ApiResponse<School>> CreateSchoolAsync(School school);
    }
}

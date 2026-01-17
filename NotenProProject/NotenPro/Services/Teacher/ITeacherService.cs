using HTLKrems.GradeManagement.Models;

namespace HTLKrems.GradeManagement.Services;

public interface ITeacherService
{
    Task<List<Teacher>> GetAllTeachersAsync();
    Task<ApiResponse<Teacher>> CreateTeacherAsync(Teacher teacher);
    Task<ApiResponse<bool>> DeleteTeacherAsync(string id);
    Task<List<Teacher>> GetTeachersAsync();
}

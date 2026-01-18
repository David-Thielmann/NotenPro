using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public interface IUserService
{
    Task<List<UserDto>> GetStudentsByClassAsync(string classId);
}
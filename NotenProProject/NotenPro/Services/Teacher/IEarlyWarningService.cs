using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public interface IEarlyWarningService
{
    Task<List<EarlyWarningDto>> GetEarlyWarningsAsync();
    Task<List<EarlyWarningDto>> GetTeacherWarningsAsync(string teacherId);
    Task<List<EarlyWarningDto>> GetPendingWarningsAsync(string teacherId);
    Task<ApiResponse<bool>> CreateWarningAsync(string teacherId, CreateEarlyWarningRequest request);
    Task<ApiResponse<bool>> SendWarningsAsync(SendEarlyWarningsRequest request);
    Task<Dictionary<string, int>?> GetSubjectStatisticsAsync(string subjectId);
}
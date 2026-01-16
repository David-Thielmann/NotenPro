using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public interface IGradeService
{
    Task<List<Grade>> GetMyGradesAsync();
    Task<List<Grade>> GetRecentGradesAsync(int count);
    Task<List<SubjectAverageDto>> GetSubjectAveragesAsync();

    /// <summary>
    /// PDF-Export der eigenen Noten (serverseitig erzeugt, Client lädt nur bytes).
    /// </summary>
    Task<byte[]> ExportMyGradesPdfAsync();

    Task<ApiResponse<bool>> SaveGradesAsync(string testId, List<StudentGradeEntry> grades);
    Task<List<Grade>> GetGradesByTestAsync(string testId);
    Task<ApiResponse<Grade>> SaveGradeAsync(Grade grade);
    Task<ApiResponse<List<Grade>>> SaveGradesBulkAsync(List<Grade> grades);
}
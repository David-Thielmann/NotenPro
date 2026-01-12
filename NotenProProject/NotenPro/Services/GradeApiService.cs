using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using NotenPro.Api.DTOs;

namespace HTLKrems.GradeManagement.Services;

public sealed class GradeApiService : IGradeService
{
    private readonly HttpClient _http;
    private readonly ICurrentUserService _currentUser;

    public GradeApiService(IHttpClientFactory factory, ICurrentUserService currentUser)
    {
        _http = factory.CreateClient("NotenProApi");
        _currentUser = currentUser;
    }

    public async Task<List<Grade>> GetMyGradesAsync()
    {
        var me = await _currentUser.GetMeAsync();
        var dtos = await _http.GetFromJsonAsync<List<GradeDto>>($"api/grades/student/{me.Id}")
                   ?? new List<GradeDto>();

        return dtos.Select(Map).ToList();
    }

    public async Task<List<Grade>> GetRecentGradesAsync(int count)
    {
        var grades = await GetMyGradesAsync();
        return grades
            .OrderByDescending(g => g.Date)
            .Take(count)
            .ToList();
    }

    public async Task<List<SubjectAverage>> GetSubjectAveragesAsync()
    {
        var grades = await GetMyGradesAsync();

        return grades
            .Where(g => g.GradeValue > 0)
            .GroupBy(g => g.Subject)
            .Select(g => new SubjectAverage
            {
                Name = g.Key,
                Average = g.Average(x => x.GradeValue),
                TestCount = g.Count()
            })
            .OrderBy(x => x.Name)
            .ToList();
    }

    // Für Student-Dashboard nicht nötig – lassen wir bewusst "noch" Dummy/Not Implemented,
    // damit wir nicht alles auf einmal machen.
    public Task<ApiResponse<bool>> SaveGradesAsync(string testId, List<StudentGradeEntry> grades)
        => Task.FromResult(new ApiResponse<bool> { Success = false, Message = "Not implemented yet." });

    public Task<List<Grade>> GetGradesByTestAsync(string testId)
        => Task.FromResult(new List<Grade>());

    public Task<ApiResponse<Grade>> SaveGradeAsync(Grade grade)
        => Task.FromResult(new ApiResponse<Grade> { Success = false, Message = "Not implemented yet." });

    public Task<ApiResponse<List<Grade>>> SaveGradesBulkAsync(List<Grade> grades)
        => Task.FromResult(new ApiResponse<List<Grade>> { Success = false, Message = "Not implemented yet." });

    private static Grade Map(GradeDto d)
    {
        var gradeValue = d.GradeValue ?? 0m;

        return new Grade
        {
            Id = d.Id,
            StudentId = d.StudentId,
            TestId = d.TestId,
            Subject = d.Subject,
            TestName = d.TestName,
            GradeValue = gradeValue,
            Points = d.Points,
            MaxPoints = d.MaxPoints,
            Comment = d.Comment,
            Date = d.Date,
            Status = Enum.TryParse<GradeStatus>(d.Status, true, out var s) ? s : GradeStatus.Graded
        };
    }
}

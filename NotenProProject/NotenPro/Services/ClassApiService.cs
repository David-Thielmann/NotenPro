using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public class ClassApiService : IClassService
{
    private readonly HttpClient _httpClient;

    public ClassApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Class>> GetAllClassesAsync()
    {
        var dtos = await _httpClient.GetFromJsonAsync<List<ClassDto>>("api/classes") ?? new();

        return dtos.Select(d => new Class
        {
            Id = d.Id,
            Name = d.Name,
            ClassTeacherId = d.TeacherId ?? string.Empty,
            TeacherName = d.TeacherName,
            StudentCount = d.StudentCount,
            AverageGrade = d.AverageGrade
        }).ToList();
    }

    // Aktuell gibt es keinen speziellen "my"-Endpoint. Daher: alle Klassen liefern.
    public Task<List<Class>> GetMyClassesAsync() => GetAllClassesAsync();

    public async Task<ApiResponse<Class>> CreateClassAsync(Class cls)
    {
        var dto = new ClassDto
        {
            Id = cls.Id,
            Name = cls.Name,
            TeacherId = string.IsNullOrWhiteSpace(cls.ClassTeacherId) ? null : cls.ClassTeacherId
        };

        var res = await _httpClient.PostAsJsonAsync("api/classes", dto);
        if (!res.IsSuccessStatusCode)
        {
            var msg = await res.Content.ReadAsStringAsync();
            return new ApiResponse<Class> { Success = false, Message = msg, ErrorMessage = msg };
        }

        var created = await res.Content.ReadFromJsonAsync<ClassDto>();
        if (created != null)
        {
            cls.Id = created.Id;
            cls.TeacherName = created.TeacherName;
            cls.StudentCount = created.StudentCount;
            cls.AverageGrade = created.AverageGrade;
        }

        return new ApiResponse<Class> { Success = true, Data = cls, Message = "OK" };
    }

    public Task<List<Class>> GetClassesAsync() => GetAllClassesAsync();
}

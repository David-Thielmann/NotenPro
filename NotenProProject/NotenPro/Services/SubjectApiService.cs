using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public class SubjectApiService : ISubjectService
{
    private readonly HttpClient _httpClient;

    public SubjectApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Subject>> GetAllSubjectsAsync()
    {
        var dtos = await _httpClient.GetFromJsonAsync<List<SubjectDto>>("api/subjects") ?? new();
        return dtos.Select(d => new Subject
        {
            Id = d.Id,
            Name = d.Name,
            Description = d.Description ?? string.Empty,
            IsActive = d.IsActive
        }).ToList();
    }

    // Aktuell gibt es keinen "my"-Endpoint.
    public Task<List<Subject>> GetMySubjectsAsync() => GetAllSubjectsAsync();

    public async Task<ApiResponse<Subject>> CreateSubjectAsync(Subject subject)
    {
        var dto = new SubjectDto
        {
            Id = subject.Id,
            Name = subject.Name,
            Description = subject.Description,
            IsActive = subject.IsActive
        };

        var res = await _httpClient.PostAsJsonAsync("api/subjects", dto);
        if (!res.IsSuccessStatusCode)
        {
            var msg = await res.Content.ReadAsStringAsync();
            return new ApiResponse<Subject> { Success = false, Message = msg, ErrorMessage = msg };
        }

        var created = await res.Content.ReadFromJsonAsync<SubjectDto>();
        if (created != null)
        {
            subject.Id = created.Id;
        }

        return new ApiResponse<Subject> { Success = true, Data = subject, Message = "OK" };
    }

    public async Task<ApiResponse<bool>> DeleteSubjectAsync(string id)
    {
        var res = await _httpClient.DeleteAsync($"api/subjects/{id}");
        if (!res.IsSuccessStatusCode)
        {
            var msg = await res.Content.ReadAsStringAsync();
            return new ApiResponse<bool> { Success = false, Data = false, Message = msg, ErrorMessage = msg };
        }

        return new ApiResponse<bool> { Success = true, Data = true, Message = "OK" };
    }

    public Task<List<Subject>> GetSubjectsAsync() => GetAllSubjectsAsync();
}

using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public class TeacherApiService : ITeacherService
{
    private readonly HttpClient _httpClient;

    public TeacherApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<Teacher>> GetAllTeachersAsync()
    {
        var dtos = await _httpClient.GetFromJsonAsync<List<UserDto>>("api/users/teachers")
                   ?? new List<UserDto>();

        return dtos.Select(t => new Teacher
        {
            Id = t.Id,
            Name = t.Name,
            Email = t.Email,
            Subjects = new List<string>(),
            IsActive = t.IsActive
        }).ToList();
    }

    public async Task<ApiResponse<Teacher>> CreateTeacherAsync(Teacher teacher)
    {
        var req = new CreateUserRequest
        {
            Name = teacher.Name,
            Email = teacher.Email,
            Password = "Password123!",
            Role = "Teacher"
        };

        var response = await _httpClient.PostAsJsonAsync("api/users", req);
        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            return new ApiResponse<Teacher>
            {
                Success = false,
                ErrorMessage = $"HTTP {(int)response.StatusCode}: {msg}"
            };
        }

        var dto = await response.Content.ReadFromJsonAsync<UserDto>();
        if (dto == null)
        {
            return new ApiResponse<Teacher>
            {
                Success = false,
                ErrorMessage = "Leerer Response"
            };
        }

        return new ApiResponse<Teacher>
        {
            Success = true,
            Data = new Teacher
            {
                Id = dto.Id,
                Name = dto.Name,
                Email = dto.Email,
                Subjects = teacher.Subjects ?? new List<string>(),
                IsActive = dto.IsActive
            }
        };
    }

    public async Task<ApiResponse<bool>> DeleteTeacherAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"api/users/{id}");
        if (!response.IsSuccessStatusCode)
        {
            var msg = await response.Content.ReadAsStringAsync();
            return new ApiResponse<bool>
            {
                Success = false,
                ErrorMessage = $"HTTP {(int)response.StatusCode}: {msg}"
            };
        }

        return new ApiResponse<bool>
        {
            Success = true,
            Data = true
        };
    }

    public Task<List<Teacher>> GetTeachersAsync()
    {
        return GetAllTeachersAsync();
    }

}

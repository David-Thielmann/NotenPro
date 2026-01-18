using System.Net.Http.Json;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public class UserApiService : IUserService
{
    private readonly HttpClient _httpClient;

    public UserApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<UserDto>> GetStudentsByClassAsync(string classId)
    {
        if (string.IsNullOrWhiteSpace(classId))
            return new List<UserDto>();

        // API: GET api/users/students?classId=...
        return await _httpClient.GetFromJsonAsync<List<UserDto>>($"api/users/students?classId={Uri.EscapeDataString(classId)}")
               ?? new List<UserDto>();
    }
}
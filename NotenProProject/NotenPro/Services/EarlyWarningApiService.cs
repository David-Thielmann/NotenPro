using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public class EarlyWarningApiService : IEarlyWarningService
{
    private readonly HttpClient _httpClient;

    public EarlyWarningApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
    }

    public async Task<List<EarlyWarningDto>> GetEarlyWarningsAsync()
        => await _httpClient.GetFromJsonAsync<List<EarlyWarningDto>>("api/earlywarnings") ?? new();

    public async Task<List<EarlyWarningDto>> GetTeacherWarningsAsync(string teacherId)
    {
        if (string.IsNullOrWhiteSpace(teacherId)) return new();
        return await _httpClient.GetFromJsonAsync<List<EarlyWarningDto>>(
            $"api/earlywarnings/teacher/{Uri.EscapeDataString(teacherId)}"
        ) ?? new();
    }

    public async Task<List<EarlyWarningDto>> GetPendingWarningsAsync(string teacherId)
    {
        var url = string.IsNullOrWhiteSpace(teacherId)
            ? "api/earlywarnings/pending"
            : $"api/earlywarnings/pending?teacherId={Uri.EscapeDataString(teacherId)}";

        return await _httpClient.GetFromJsonAsync<List<EarlyWarningDto>>(url) ?? new();
    }

    public async Task<ApiResponse<bool>> CreateWarningAsync(string teacherId, CreateEarlyWarningRequest request)
    {
        if (string.IsNullOrWhiteSpace(teacherId))
            return new ApiResponse<bool>
            {
                Success = false,
                Data = false,
                Message = "teacherId fehlt",
                ErrorMessage = "teacherId fehlt"
            };

        var res = await _httpClient.PostAsJsonAsync(
            $"api/earlywarnings?teacherId={Uri.EscapeDataString(teacherId)}",
            request
        );

        if (!res.IsSuccessStatusCode)
        {
            var msg = await res.Content.ReadAsStringAsync();
            return new ApiResponse<bool> { Success = false, Data = false, Message = msg, ErrorMessage = msg };
        }

        return new ApiResponse<bool> { Success = true, Data = true, Message = "OK" };
    }

    public async Task<ApiResponse<bool>> SendWarningsAsync(SendEarlyWarningsRequest request)
    {
        var res = await _httpClient.PostAsJsonAsync("api/earlywarnings/send", request);

        if (!res.IsSuccessStatusCode)
        {
            var msg = await res.Content.ReadAsStringAsync();
            return new ApiResponse<bool> { Success = false, Data = false, Message = msg, ErrorMessage = msg };
        }

        return new ApiResponse<bool> { Success = true, Data = true, Message = "OK" };
    }

    // ✅ statt WarningStatisticsDto
    public async Task<Dictionary<string, int>?> GetSubjectStatisticsAsync(string subjectId)
    {
        if (string.IsNullOrWhiteSpace(subjectId)) return null;

        return await _httpClient.GetFromJsonAsync<Dictionary<string, int>>(
            $"api/earlywarnings/statistics/subject/{Uri.EscapeDataString(subjectId)}"
        );
    }
}

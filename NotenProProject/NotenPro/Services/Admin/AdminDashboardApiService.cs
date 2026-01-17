using System.Net.Http.Json;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services;

public class AdminDashboardApiService : IAdminDashboardService
{
    private readonly HttpClient _httpClient;

    public AdminDashboardApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        Console.WriteLine($"DEBUG: AdminDashboardApiService created with BaseAddress: {_httpClient.BaseAddress}");
    }

    public async Task<AdminDashboardStatsDto?> GetStatsAsync()
    {
        // Endpoint is provided by NotenPro.Api/Controllers/AdminDashboardController
        return await _httpClient.GetFromJsonAsync<AdminDashboardStatsDto>("api/admin/dashboard/stats");
    }
}
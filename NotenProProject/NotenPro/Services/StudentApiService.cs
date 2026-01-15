using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using HTLKrems.GradeManagement.Services;
using NotenPro.Shared.DTOs;

public class StudentApiService : IStudentService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;

    public StudentApiService(IHttpClientFactory httpClientFactory, ICurrentUserService currentUserService)
    {
        // 🔥 WICHTIG: "ApiClient" als String übergeben!
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _currentUserService = currentUserService;
        Console.WriteLine($"DEBUG: StudentApiService created with BaseAddress: {_httpClient.BaseAddress}");
    }

    public async Task<StudentDashboardStats> GetDashboardStatsAsync()
    {
        try
        {
            Console.WriteLine("DEBUG: StudentApiService.GetDashboardStatsAsync()");
            Console.WriteLine($"DEBUG: HttpClient BaseAddress: {_httpClient.BaseAddress}");
            
            var currentUser = await _currentUserService.GetMeAsync();
            
            if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
            {
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");
            }

            Console.WriteLine($"DEBUG: Using real user ID: {currentUser.Id}");
            
            var response = await _httpClient.GetAsync($"api/students/{currentUser.Id}/dashboard/stats");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Dashboard Stats API-Fehler ({response.StatusCode}): {errorContent}");
            }
            
            var stats = await response.Content.ReadFromJsonAsync<StudentDashboardStats>();
            return stats;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: StudentApiService error: {ex.Message}");
            throw;
        }
    }
}
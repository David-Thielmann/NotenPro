using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using HTLKrems.GradeManagement.Services;
using NotenPro.Shared.DTOs;

public class NotificationApiService : INotificationService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;

    public NotificationApiService(IHttpClientFactory httpClientFactory, ICurrentUserService currentUserService)
    {
        // 🔥 WICHTIG: DENSELBEN NAMEN WIE IN PROGRAM.CS VERWENDEN!
        // Wenn in Program.cs "ApiClient" steht:
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        
        // ODER wenn in Program.cs "NotenProApi" steht:
        // _httpClient = httpClientFactory.CreateClient("NotenProApi");
        
        _currentUserService = currentUserService;
        Console.WriteLine($"DEBUG: NotificationApiService created with BaseAddress: {_httpClient?.BaseAddress}");
    }

    public async Task<List<Notification>> GetMyNotificationsAsync()
    {
        try
        {
            Console.WriteLine("DEBUG: NotificationApiService.GetMyNotificationsAsync()");
            Console.WriteLine($"DEBUG: HttpClient BaseAddress: {_httpClient.BaseAddress}");
            
            var currentUser = await _currentUserService.GetMeAsync();
            
            if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
            {
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");
            }
            
            var response = await _httpClient.GetAsync($"api/notifications/user/{currentUser.Id}");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Notifications API-Fehler ({response.StatusCode}): {errorContent}");
            }
            
            var result = await response.Content.ReadFromJsonAsync<List<Notification>>() ?? new();
            Console.WriteLine($"DEBUG: Notifications loaded: {result.Count}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: GetMyNotificationsAsync error: {ex.Message}");
            throw;
        }
    }

    public async Task<int> GetUnreadCountAsync()
    {
        try
        {
            var currentUser = await _currentUserService.GetMeAsync();
            
            if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
            {
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");
            }
            
            var response = await _httpClient.GetAsync($"api/notifications/user/{currentUser.Id}/unread/count");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Notifications API-Fehler ({response.StatusCode}): {errorContent}");
            }
            
            var result = await response.Content.ReadFromJsonAsync<int>();
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: GetUnreadCount error: {ex.Message}");
            throw;
        }
    }

    public async Task<bool> MarkAsReadAsync(string id)
    {
        try
        {
            Console.WriteLine("DEBUG: NotificationApiService.MarkAsReadAsync()");
            
            var response = await _httpClient.PutAsync($"api/notifications/{id}/read", null);
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"MarkAsRead API-Fehler ({response.StatusCode}): {errorContent}");
            }
            
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: MarkAsReadAsync error: {ex.Message}");
            throw;
        }
    }
}
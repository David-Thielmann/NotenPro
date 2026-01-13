using System.Net.Http.Json;
using HTLKrems.GradeManagement.Services;
using NotenPro.Api.DTOs;

public class CurrentUserApiService : ICurrentUserService
{
    private readonly HttpClient _httpClient;

    public CurrentUserApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        Console.WriteLine($"DEBUG: CurrentUserApiService created with BaseAddress: {_httpClient.BaseAddress}");
    }

    public async Task<AuthMeDto> GetMeAsync()
    {
        try
        {
            Console.WriteLine("DEBUG: CurrentUserApiService.GetMeAsync()");
            Console.WriteLine($"DEBUG: HttpClient BaseAddress: {_httpClient.BaseAddress}");
            
            var response = await _httpClient.GetAsync("api/auth/me");
            
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"User API-Fehler ({response.StatusCode}): {errorContent}");
            }
            
            var authMeDto = await response.Content.ReadFromJsonAsync<AuthMeDto>();
            
            if (authMeDto == null || string.IsNullOrEmpty(authMeDto.Id))
            {
                throw new Exception("Ungültige Benutzerdaten erhalten");
            }
            
            Console.WriteLine($"DEBUG: GetMeAsync - User ID: {authMeDto.Id}, Name: {authMeDto.Name}");
            return authMeDto;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: GetMeAsync error: {ex.Message}");
            throw;
        }
    }
}
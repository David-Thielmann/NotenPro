using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using HTLKrems.GradeManagement.Services;
using NotenPro.Api.DTOs;

namespace HTLKrems.GradeManagement.Services
{
    public interface IGradeService
    {
        Task<List<Grade>> GetMyGradesAsync();
        Task<List<Grade>> GetRecentGradesAsync(int count);
        Task<List<SubjectAverage>> GetSubjectAveragesAsync();
        Task<ApiResponse<bool>> SaveGradesAsync(string testId, List<StudentGradeEntry> grades);
        Task<List<Grade>> GetGradesByTestAsync(string testId);
        Task<ApiResponse<Grade>> SaveGradeAsync(Grade grade);
        Task<ApiResponse<List<Grade>>> SaveGradesBulkAsync(List<Grade> grades);
    }

    public class GradeApiService : IGradeService
    {
        private readonly HttpClient _httpClient;
        private readonly ICurrentUserService _currentUserService;

        public GradeApiService(HttpClient httpClient, ICurrentUserService currentUserService)
        {
            _httpClient = httpClient;
            _currentUserService = currentUserService;
        }

        public async Task<List<Grade>> GetMyGradesAsync()
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.GetMyGradesAsync()");
                
                var currentUser = await _currentUserService.GetMeAsync();
                
                if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
                {
                    throw new Exception("Benutzerdaten konnten nicht geladen werden.");
                }
                
                var response = await _httpClient.GetAsync($"api/grades/student/{currentUser.Id}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Grades API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<List<Grade>>() ?? new();
                Console.WriteLine($"DEBUG: My grades loaded: {result.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: GetMyGradesAsync error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Grade>> GetRecentGradesAsync(int count)
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.GetRecentGradesAsync()");
                
                var currentUser = await _currentUserService.GetMeAsync();
                
                if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
                {
                    throw new Exception("Benutzerdaten konnten nicht geladen werden.");
                }
                
                var response = await _httpClient.GetAsync($"api/grades/student/{currentUser.Id}?count={count}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Grades API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<List<Grade>>() ?? new();
                Console.WriteLine($"DEBUG: Real grades loaded: {result.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: GetRecentGrades error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<SubjectAverage>> GetSubjectAveragesAsync()
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.GetSubjectAveragesAsync()");
                
                var currentUser = await _currentUserService.GetMeAsync();
                
                if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
                {
                    throw new Exception("Benutzerdaten konnten nicht geladen werden.");
                }
                
                var response = await _httpClient.GetAsync($"api/grades/student/{currentUser.Id}/averages");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Averages API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<List<SubjectAverage>>() ?? new();
                Console.WriteLine($"DEBUG: Real averages loaded: {result.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: GetSubjectAverages error: {ex.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<bool>> SaveGradesAsync(string testId, List<StudentGradeEntry> grades)
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.SaveGradesAsync()");
                
                var response = await _httpClient.PostAsJsonAsync($"api/grades/test/{testId}", grades);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"SaveGrades API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>();
                Console.WriteLine($"DEBUG: SaveGrades success: {result?.Success}");
                return result ?? new ApiResponse<bool> { Success = false, Message = "Keine Antwort erhalten" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: SaveGradesAsync error: {ex.Message}");
                return new ApiResponse<bool> { Success = false, Message = ex.Message };
            }
        }

        public async Task<List<Grade>> GetGradesByTestAsync(string testId)
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.GetGradesByTestAsync()");
                
                var response = await _httpClient.GetAsync($"api/grades/test/{testId}");
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"GetGradesByTest API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<List<Grade>>() ?? new();
                Console.WriteLine($"DEBUG: Grades by test loaded: {result.Count}");
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: GetGradesByTestAsync error: {ex.Message}");
                throw;
            }
        }

        public async Task<ApiResponse<Grade>> SaveGradeAsync(Grade grade)
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.SaveGradeAsync()");
                
                var response = await _httpClient.PostAsJsonAsync("api/grades", grade);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"SaveGrade API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<Grade>>();
                Console.WriteLine($"DEBUG: SaveGrade success: {result?.Success}");
                return result ?? new ApiResponse<Grade> { Success = false, Message = "Keine Antwort erhalten" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: SaveGradeAsync error: {ex.Message}");
                return new ApiResponse<Grade> { Success = false, Message = ex.Message };
            }
        }

        public async Task<ApiResponse<List<Grade>>> SaveGradesBulkAsync(List<Grade> grades)
        {
            try
            {
                Console.WriteLine("DEBUG: GradeApiService.SaveGradesBulkAsync()");
                
                var response = await _httpClient.PostAsJsonAsync("api/grades/bulk", grades);
                
                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadFromJsonAsync<string>() ?? string.Empty;
                    throw new HttpRequestException($"SaveGradesBulk API-Fehler ({response.StatusCode}): {errorContent}");
                }
                
                var result = await response.Content.ReadFromJsonAsync<ApiResponse<List<Grade>>>();
                Console.WriteLine($"DEBUG: SaveGradesBulk success: {result?.Success}");
                return result ?? new ApiResponse<List<Grade>> { Success = false, Message = "Keine Antwort erhalten" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"DEBUG: SaveGradesBulkAsync error: {ex.Message}");
                return new ApiResponse<List<Grade>> { Success = false, Message = ex.Message };
            }
        }
    }

    public class SubjectAverage
    {
        public string Name { get; set; } = "";
        public decimal Average { get; set; }
        public int TestCount { get; set; }
    }
}
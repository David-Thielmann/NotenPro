using System.Net.Http.Json;
using System.Text.Json;
using HTLKrems.GradeManagement.Models;
using HTLKrems.GradeManagement.Services;
using HTLKrems.GradeManagement.Services.Json;
using NotenPro.Shared.DTOs;

public class GradeApiService : IGradeService
{
    private readonly HttpClient _httpClient;
    private readonly ICurrentUserService _currentUserService;

    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web)
    {
        Converters =
        {
            new FlexibleEnumConverter<GradeStatus>()
        }
    };

    public GradeApiService(IHttpClientFactory httpClientFactory, ICurrentUserService currentUserService)
    {
        _httpClient = httpClientFactory.CreateClient("ApiClient");
        _currentUserService = currentUserService;
        Console.WriteLine($"DEBUG: GradeApiService created with BaseAddress: {_httpClient.BaseAddress}");
    }

    public async Task<List<Grade>> GetMyGradesAsync()
    {
        try
        {
            Console.WriteLine("DEBUG: GradeApiService.GetMyGradesAsync()");
            Console.WriteLine($"DEBUG: HttpClient BaseAddress: {_httpClient.BaseAddress}");

            var currentUser = await _currentUserService.GetMeAsync();

            if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");

            var response = await _httpClient.GetAsync($"api/grades/student/{currentUser.Id}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Grades API-Fehler ({response.StatusCode}): {errorContent}");
            }

            // IMPORTANT:
            // API returns GradeDto where GradeValue can be NULL (Pending/Absent).
            // Our UI model Grade has a non-nullable decimal -> NULL would become 0 and break averages.
            var dtos = await response.Content.ReadFromJsonAsync<List<GradeDto>>(JsonOptions) ?? new();
            var result = dtos.Select(MapToUiModel).ToList();
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
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");

            var response = await _httpClient.GetAsync($"api/grades/student/{currentUser.Id}/recent?count={count}");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Grades API-Fehler ({response.StatusCode}): {errorContent}");
            }

            var dtos = await response.Content.ReadFromJsonAsync<List<GradeDto>>(JsonOptions) ?? new();
            var result = dtos.Select(MapToUiModel).ToList();
            Console.WriteLine($"DEBUG: Real grades loaded: {result.Count}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: GetRecentGrades error: {ex.Message}");
            throw;
        }
    }

    public async Task<List<SubjectAverageDto>> GetSubjectAveragesAsync()
    {
        try
        {
            Console.WriteLine("DEBUG: GradeApiService.GetSubjectAveragesAsync()");

            var currentUser = await _currentUserService.GetMeAsync();

            if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");

            var response = await _httpClient.GetAsync($"api/grades/student/{currentUser.Id}/averages");

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Averages API-Fehler ({response.StatusCode}): {errorContent}");
            }

            // API liefert { name, average, testCount }
            var result = await response.Content.ReadFromJsonAsync<List<SubjectAverageDto>>(JsonOptions) ?? new();
            Console.WriteLine($"DEBUG: Real averages loaded: {result.Count}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: GetSubjectAverages error: {ex.Message}");
            throw;
        }
    }

    public async Task<byte[]> ExportMyGradesPdfAsync()
    {
        try
        {
            Console.WriteLine("DEBUG: GradeApiService.ExportMyGradesPdfAsync()");

            var currentUser = await _currentUserService.GetMeAsync();
            if (currentUser == null || string.IsNullOrEmpty(currentUser.Id))
                throw new Exception("Benutzerdaten konnten nicht geladen werden.");

            var response = await _httpClient.GetAsync($"api/grades/export/student/{currentUser.Id}");
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"PDF Export API-Fehler ({response.StatusCode}): {errorContent}");
            }

            return await response.Content.ReadAsByteArrayAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: ExportMyGradesPdfAsync error: {ex.Message}");
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

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<bool>>(JsonOptions);
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

            // Frühwarnungen haben bisher mit "" gearbeitet -> das darf nicht in /test/ landen.
            // Wenn kein TestId vorhanden ist, holen wir alle Noten.
            var url = string.IsNullOrWhiteSpace(testId)
                ? "api/grades"
                : $"api/grades/test/{testId}";

            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"GetGradesByTest API-Fehler ({response.StatusCode}): {errorContent}");
            }

            var dtos = await response.Content.ReadFromJsonAsync<List<GradeDto>>(JsonOptions) ?? new();
            var result = dtos.Select(MapToUiModel).ToList();
            Console.WriteLine($"DEBUG: Grades by test loaded: {result.Count}");
            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: GetGradesByTestAsync error: {ex.Message}");
            throw;
        }
    }

    private static Grade MapToUiModel(GradeDto dto)
    {
        Enum.TryParse(dto.Status, ignoreCase: true, out GradeStatus parsedStatus);

        return new Grade
        {
            Id = dto.Id,
            StudentId = dto.StudentId,
            TestId = dto.TestId,
            Subject = dto.Subject,
            TestName = dto.TestName,
            GradeValue = dto.GradeValue ?? 0m,
            Points = dto.Points,
            MaxPoints = dto.MaxPoints,
            Date = dto.Date,
            Status = parsedStatus,
            Comment = dto.Comment
        };
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

            var result = await response.Content.ReadFromJsonAsync<ApiResponse<Grade>>(JsonOptions);
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

            if (grades == null || grades.Count == 0)
                return new ApiResponse<List<Grade>> { Success = false, Message = "Keine Noten zum Speichern" };

            var testId = grades.FirstOrDefault(g => !string.IsNullOrWhiteSpace(g.TestId))?.TestId;
            if (string.IsNullOrWhiteSpace(testId))
                return new ApiResponse<List<Grade>> { Success = false, Message = "TestId fehlt" };

            // API erwartet BulkGradeRequest
            var request = new BulkGradeRequest
            {
                TestId = testId,
                Grades = grades.Select(g => new StudentGradeInput
                {
                    StudentId = g.StudentId,
                    GradeValue = g.Status == GradeStatus.Graded && g.GradeValue > 0m ? g.GradeValue : null,
                    Points = g.Points,
                    Status = g.Status.ToString(),
                    Comment = g.Comment
                }).ToList()
            };

            var response = await _httpClient.PostAsJsonAsync("api/grades/bulk", request, JsonOptions);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"SaveGradesBulk API-Fehler ({response.StatusCode}): {errorContent}");
            }

            // Controller liefert aktuell nur { message = "..." } -> wir geben trotzdem Success zurück
            Console.WriteLine("DEBUG: SaveGradesBulk success: true");
            return new ApiResponse<List<Grade>> { Success = true, Data = grades, Message = "Gespeichert" };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"DEBUG: SaveGradesBulkAsync error: {ex.Message}");
            return new ApiResponse<List<Grade>> { Success = false, Message = ex.Message };
        }
    }
}

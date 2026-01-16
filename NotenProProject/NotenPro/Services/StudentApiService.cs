using System.Net.Http.Json;
using NotenPro.Shared.DTOs;

namespace HTLKrems.GradeManagement.Services
{
    public class StudentApiService : IStudentService
    {
        private readonly HttpClient _httpClient;
        private readonly ICurrentUserService _currentUserService;

        public StudentApiService(IHttpClientFactory httpClientFactory, ICurrentUserService currentUserService)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
            _currentUserService = currentUserService;
        }

        public async Task<StudentDashboardStats> GetDashboardStatsAsync()
        {
            var me = await _currentUserService.GetMeAsync();
            if (me == null || string.IsNullOrWhiteSpace(me.Id))
                throw new Exception("Benutzer nicht geladen.");

            return await _httpClient.GetFromJsonAsync<StudentDashboardStats>(
                $"api/students/{me.Id}/dashboard/stats"
            ) ?? new StudentDashboardStats();
        }

        public async Task<StudentProfileDto> GetMyProfileAsync()
        {
            var me = await _currentUserService.GetMeAsync();
            if (me == null || string.IsNullOrWhiteSpace(me.Id))
                throw new Exception("Benutzer nicht geladen.");

            return await _httpClient.GetFromJsonAsync<StudentProfileDto>(
                $"api/students/{me.Id}/profile"
            ) ?? new StudentProfileDto { Id = me.Id, ClassName = "-" };
        }
    }

    // Lass den Typ im Client ruhig so stehen, wenn du ihn im UI nutzt:
    public class StudentDashboardStats
    {
        public double AverageGrade { get; set; }
        public int UngradedTests { get; set; }
        public int UnreadNotifications { get; set; }
        public string ClassName { get; set; } = "";
    }
}
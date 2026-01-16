using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;

namespace HTLKrems.GradeManagement.Services
{
    public class TestApiService : ITestService
    {
        private readonly HttpClient _httpClient;

        public TestApiService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("ApiClient");
        }

        public async Task<List<Test>> GetMyTestsAsync()
            => await _httpClient.GetFromJsonAsync<List<Test>>("api/tests/my") ?? new(); // TODO route

        public async Task<Test?> GetTestByIdAsync(string id)
            => await _httpClient.GetFromJsonAsync<Test>($"api/tests/{id}"); // TODO route

        public async Task<ApiResponse<Test>> CreateTestAsync(CreateTestRequest request)
        {
            var res = await _httpClient.PostAsJsonAsync("api/tests", request); // TODO route
            return await res.Content.ReadFromJsonAsync<ApiResponse<Test>>()
                   ?? new ApiResponse<Test> { Success = false, Message = "Leere Antwort" };
        }

        public async Task<ApiResponse<bool>> DeleteTestAsync(string id)
        {
            var res = await _httpClient.DeleteAsync($"api/tests/{id}"); // TODO route
            return await res.Content.ReadFromJsonAsync<ApiResponse<bool>>()
                   ?? new ApiResponse<bool> { Success = false, Message = "Leere Antwort" };
        }
    }
}
using System.Net.Http.Json;
using NotenPro.Api.DTOs;

namespace HTLKrems.GradeManagement.Services;

public sealed class CurrentUserApiService : ICurrentUserService
{
    private readonly HttpClient _http;
    private AuthMeDto? _cache;

    public CurrentUserApiService(IHttpClientFactory factory)
    {
        _http = factory.CreateClient("NotenProApi");
    }

    public async Task<AuthMeDto> GetMeAsync(bool forceRefresh = false)
    {
        if (!forceRefresh && _cache != null)
            return _cache;

        var me = await _http.GetFromJsonAsync<AuthMeDto>("api/auth/me");
        if (me == null || string.IsNullOrWhiteSpace(me.Id))
            throw new InvalidOperationException("api/auth/me returned no valid user.");

        _cache = me;
        return me;
    }
}
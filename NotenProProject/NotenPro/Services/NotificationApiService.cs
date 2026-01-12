using System.Net.Http.Json;
using HTLKrems.GradeManagement.Models;
using NotenPro.Api.DTOs;

namespace HTLKrems.GradeManagement.Services;

public sealed class NotificationApiService : INotificationService
{
    private readonly HttpClient _http;
    private readonly ICurrentUserService _currentUser;

    public NotificationApiService(IHttpClientFactory factory, ICurrentUserService currentUser)
    {
        _http = factory.CreateClient("NotenProApi");
        _currentUser = currentUser;
    }

    public async Task<List<Notification>> GetMyNotificationsAsync()
    {
        var me = await _currentUser.GetMeAsync();
        var dtos = await _http.GetFromJsonAsync<List<NotificationDto>>($"api/notifications/user/{me.Id}")
                   ?? new List<NotificationDto>();

        return dtos
            .OrderByDescending(n => n.Timestamp)
            .Select(d => new Notification
            {
                Id = d.Id,
                UserId = d.UserId,
                Title = d.Title,
                Message = d.Message,
                Timestamp = d.Timestamp,
                IsRead = d.IsRead,
                Type = Enum.TryParse<NotificationType>(d.Type, true, out var t) ? t : NotificationType.Info
            })
            .ToList();
    }

    public async Task<int> GetUnreadCountAsync()
    {
        var me = await _currentUser.GetMeAsync();
        var count = await _http.GetFromJsonAsync<int>($"api/notifications/user/{me.Id}/count");
        return count;
    }

    public async Task<bool> MarkAsReadAsync(string id)
    {
        // du hast in der API mehrere Varianten – die einfachste ist PUT api/notifications/{id}/read
        var resp = await _http.PutAsync($"api/notifications/{id}/read", content: null);
        return resp.IsSuccessStatusCode;
    }
}
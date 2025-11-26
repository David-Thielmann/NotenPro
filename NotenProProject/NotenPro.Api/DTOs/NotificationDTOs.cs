namespace NotenPro.Api.DTOs;

public class NotificationDto
{
    public string Id { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public bool IsRead { get; set; }
    public DateTime Timestamp { get; set; }
}

public class CreateNotificationRequest
{
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Info";
}

public class MarkAsReadRequest
{
    public List<string> NotificationIds { get; set; } = new();
}

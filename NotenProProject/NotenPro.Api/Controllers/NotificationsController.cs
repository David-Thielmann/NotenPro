using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data;
using NotenPro.Domain.Entities;
using NotenPro.Shared.DTOs;


namespace NotenPro.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificationsController : ControllerBase
{
    private readonly NotenProDbContext _context;
    private readonly ILogger<NotificationsController> _logger;

    public NotificationsController(NotenProDbContext context, ILogger<NotificationsController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationDto>>> GetAllNotifications()
    {
        var notifications = await _context.Notifications
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type.ToString(),
                IsRead = n.IsRead,
                Timestamp = n.Timestamp
            })
            .OrderByDescending(n => n.Timestamp)
            .ToListAsync();

        return Ok(notifications);
    }

    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<NotificationDto>>> GetUserNotifications(string userId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type.ToString(),
                IsRead = n.IsRead,
                Timestamp = n.Timestamp
            })
            .OrderByDescending(n => n.Timestamp)
            .ToListAsync();

        return Ok(notifications);
    }

    [HttpGet("user/{userId}/unread")]
    public async Task<ActionResult<List<NotificationDto>>> GetUnreadNotifications(string userId)
    {
        var notifications = await _context.Notifications
            .Where(n => n.UserId == userId && !n.IsRead)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type.ToString(),
                IsRead = n.IsRead,
                Timestamp = n.Timestamp
            })
            .OrderByDescending(n => n.Timestamp)
            .ToListAsync();

        return Ok(notifications);
    }

    [HttpGet("user/{userId}/count")]
    public async Task<ActionResult<int>> GetUnreadCount(string userId)
    {
        var count = await _context.Notifications
            .CountAsync(n => n.UserId == userId && !n.IsRead);

        return Ok(count);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<NotificationDto>> GetNotification(string id)
    {
        var notification = await _context.Notifications
            .Where(n => n.Id == id)
            .Select(n => new NotificationDto
            {
                Id = n.Id,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type.ToString(),
                IsRead = n.IsRead,
                Timestamp = n.Timestamp
            })
            .FirstOrDefaultAsync();

        if (notification == null)
            return NotFound();

        return Ok(notification);
    }

    [HttpPost]
    public async Task<ActionResult<NotificationDto>> CreateNotification([FromBody] CreateNotificationRequest request)
    {
        try
        {
            if (!Enum.TryParse<NotificationType>(request.Type, out var notificationType))
            {
                notificationType = NotificationType.Info;
            }

            var notification = new NotificationEntity
            {
                UserId = request.UserId,
                Title = request.Title,
                Message = request.Message,
                Type = notificationType,
                IsRead = false,
                Timestamp = DateTime.UtcNow
            };

            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            var notificationDto = new NotificationDto
            {
                Id = notification.Id,
                UserId = notification.UserId,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type.ToString(),
                IsRead = notification.IsRead,
                Timestamp = notification.Timestamp
            };

            return CreatedAtAction(nameof(GetNotification), new { id = notification.Id }, notificationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating notification");
            return StatusCode(500, "Error creating notification");
        }
    }

    [HttpPost("broadcast")]
    public async Task<ActionResult> BroadcastNotification([FromBody] BroadcastNotificationRequest request)
    {
        try
        {
            if (!Enum.TryParse<NotificationType>(request.Type, out var notificationType))
            {
                notificationType = NotificationType.Info;
            }

            var notifications = new List<NotificationEntity>();
            foreach (var userId in request.UserIds)
            {
                notifications.Add(new NotificationEntity
                {
                    UserId = userId,
                    Title = request.Title,
                    Message = request.Message,
                    Type = notificationType,
                    IsRead = false,
                    Timestamp = DateTime.UtcNow
                });
            }

            _context.Notifications.AddRange(notifications);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Notification sent to {request.UserIds.Count} users" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error broadcasting notification");
            return StatusCode(500, "Error broadcasting notification");
        }
    }

    [HttpPut("{id}/read")]
    public async Task<ActionResult> MarkAsRead(string id)
    {
        try
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            notification.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking notification as read");
            return StatusCode(500, "Error marking notification as read");
        }
    }

    [HttpPost("mark-read")]
    public async Task<ActionResult> MarkMultipleAsRead([FromBody] MarkAsReadRequest request)
    {
        try
        {
            var notifications = await _context.Notifications
                .Where(n => request.NotificationIds.Contains(n.Id))
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Marked {notifications.Count} notifications as read" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking notifications as read");
            return StatusCode(500, "Error marking notifications as read");
        }
    }

    [HttpPost("user/{userId}/mark-all-read")]
    public async Task<ActionResult> MarkAllAsRead(string userId)
    {
        try
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId && !n.IsRead)
                .ToListAsync();

            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"Marked {notifications.Count} notifications as read" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error marking all notifications as read");
            return StatusCode(500, "Error marking all notifications as read");
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteNotification(string id)
    {
        try
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification == null)
                return NotFound();

            _context.Notifications.Remove(notification);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting notification");
            return StatusCode(500, "Error deleting notification");
        }
    }

    [HttpDelete("user/{userId}/clear")]
    public async Task<ActionResult> ClearAllUserNotifications(string userId)
    {
        try
        {
            var notifications = await _context.Notifications
                .Where(n => n.UserId == userId)
                .ToListAsync();

            _context.Notifications.RemoveRange(notifications);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Deleted {notifications.Count} notifications" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error clearing notifications");
            return StatusCode(500, "Error clearing notifications");
        }
    }
}

public class BroadcastNotificationRequest
{
    public List<string> UserIds { get; set; } = new();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public string Type { get; set; } = "Info";
}

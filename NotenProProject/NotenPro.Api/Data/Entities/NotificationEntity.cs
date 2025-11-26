using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Api.Data.Entities;

public enum NotificationType
{
    Info = 0,
    Warning = 1,
    Success = 2,
    Error = 3
}

[Table("notifications")]
public class NotificationEntity
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column("user_id")]
    [MaxLength(36)]
    public string UserId { get; set; } = string.Empty;

    [Required]
    [Column("title")]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [Column("message")]
    [MaxLength(2000)]
    public string Message { get; set; } = string.Empty;

    [Required]
    [Column("type")]
    public NotificationType Type { get; set; } = NotificationType.Info;

    [Column("is_read")]
    public bool IsRead { get; set; } = false;

    [Column("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("UserId")]
    public UserEntity User { get; set; } = null!;
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotenPro.Domain.Entities;

[Table("early_warnings")]
public class EarlyWarningEntity
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column("student_id")]
    [MaxLength(36)]
    public string StudentId { get; set; } = string.Empty;

    [Required]
    [Column("subject_id")]
    [MaxLength(36)]
    public string SubjectId { get; set; } = string.Empty;

    [Required]
    [Column("teacher_id")]
    [MaxLength(36)]
    public string TeacherId { get; set; } = string.Empty;

    [Required]
    [Column("reason")]
    [MaxLength(1000)]
    public string Reason { get; set; } = string.Empty;

    [Column("current_average")]
    [Precision(3, 2)]
    public decimal CurrentAverage { get; set; }

    [Column("is_sent")]
    public bool IsSent { get; set; } = false;

    [Column("sent_at")]
    public DateTime? SentAt { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("StudentId")]
    public UserEntity Student { get; set; } = null!;

    [ForeignKey("SubjectId")]
    public SubjectEntity Subject { get; set; } = null!;

    [ForeignKey("TeacherId")]
    public UserEntity Teacher { get; set; } = null!;
}

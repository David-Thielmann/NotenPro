using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Domain.Entities;

[Table("teacher_subjects")]
public class TeacherSubjectEntity
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column("teacher_id")]
    [MaxLength(36)]
    public string TeacherId { get; set; } = string.Empty;

    [Required]
    [Column("subject_id")]
    [MaxLength(36)]
    public string SubjectId { get; set; } = string.Empty;

    [Column("assigned_at")]
    public DateTime AssignedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("TeacherId")]
    public UserEntity Teacher { get; set; } = null!;

    [ForeignKey("SubjectId")]
    public SubjectEntity Subject { get; set; } = null!;
}

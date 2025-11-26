using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Api.Data.Entities;

[Table("student_classes")]
public class StudentClassEntity
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
    [Column("class_id")]
    [MaxLength(36)]
    public string ClassId { get; set; } = string.Empty;

    [Column("enrolled_at")]
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("StudentId")]
    public UserEntity Student { get; set; } = null!;

    [ForeignKey("ClassId")]
    public ClassEntity Class { get; set; } = null!;
}

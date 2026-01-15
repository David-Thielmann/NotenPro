using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NotenPro.Domain.Entities;

public enum GradeStatus
{
    Graded = 0,
    Pending = 1,
    Absent = 2
}

[Table("grades")]
public class GradeEntity
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
    [Column("test_id")]
    [MaxLength(36)]
    public string TestId { get; set; } = string.Empty;

    [Column("grade_value")]
    [Precision(3, 2)]
    public decimal? GradeValue { get; set; }

    [Column("points")]
    public int? Points { get; set; }

    [Column("max_points")]
    public int? MaxPoints { get; set; }

    [Required]
    [Column("status")]
    public GradeStatus Status { get; set; } = GradeStatus.Pending;

    [Column("comment")]
    [MaxLength(1000)]
    public string? Comment { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("StudentId")]
    public UserEntity Student { get; set; } = null!;

    [ForeignKey("TestId")]
    public TestEntity Test { get; set; } = null!;
}

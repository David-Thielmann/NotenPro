using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Api.Data.Entities;

public enum TestType
{
    Test = 0,
    Schularbeit = 1,
    Mitarbeit = 2,
    Hausübung = 3
}

[Table("tests")]
public class TestEntity
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column("name")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("subject_id")]
    [MaxLength(36)]
    public string SubjectId { get; set; } = string.Empty;

    [Required]
    [Column("class_id")]
    [MaxLength(36)]
    public string ClassId { get; set; } = string.Empty;

    [Required]
    [Column("teacher_id")]
    [MaxLength(36)]
    public string TeacherId { get; set; } = string.Empty;

    [Required]
    [Column("date")]
    public DateTime Date { get; set; }

    [Required]
    [Column("max_points")]
    public int MaxPoints { get; set; }

    [Required]
    [Column("type")]
    public TestType Type { get; set; }

    [Column("description")]
    [MaxLength(2000)]
    public string? Description { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("SubjectId")]
    public SubjectEntity Subject { get; set; } = null!;

    [ForeignKey("ClassId")]
    public ClassEntity Class { get; set; } = null!;

    [ForeignKey("TeacherId")]
    public UserEntity Teacher { get; set; } = null!;

    public ICollection<GradeEntity> Grades { get; set; } = new List<GradeEntity>();
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Domain.Entities;

[Table("classes")]
public class ClassEntity
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column("name")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [Column("school_id")]
    [MaxLength(36)]
    public string SchoolId { get; set; } = string.Empty;

    [Column("class_teacher_id")]
    [MaxLength(36)]
    public string? ClassTeacherId { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("SchoolId")]
    public SchoolEntity School { get; set; } = null!;

    [ForeignKey("ClassTeacherId")]
    public UserEntity? ClassTeacher { get; set; }

    public ICollection<StudentClassEntity> StudentClasses { get; set; } = new List<StudentClassEntity>();
    public ICollection<TestEntity> Tests { get; set; } = new List<TestEntity>();
}

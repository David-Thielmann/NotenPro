using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Api.Data.Entities;

[Table("subjects")]
public class SubjectEntity
{
    [Key]
    [Column("id")]
    [MaxLength(36)]
    public string Id { get; set; } = Guid.NewGuid().ToString();

    [Required]
    [Column("name")]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    [Column("description")]
    [MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    [Column("school_id")]
    [MaxLength(36)]
    public string SchoolId { get; set; } = string.Empty;

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    [ForeignKey("SchoolId")]
    public SchoolEntity School { get; set; } = null!;

    public ICollection<TestEntity> Tests { get; set; } = new List<TestEntity>();
    public ICollection<TeacherSubjectEntity> TeacherSubjects { get; set; } = new List<TeacherSubjectEntity>();
}

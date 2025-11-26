using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Api.Data.Entities;

[Table("schools")]
public class SchoolEntity
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
    [Column("location")]
    [MaxLength(200)]
    public string Location { get; set; } = string.Empty;

    [Column("status")]
    [MaxLength(50)]
    public string Status { get; set; } = "Active";

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Navigation Properties
    public ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
    public ICollection<ClassEntity> Classes { get; set; } = new List<ClassEntity>();
    public ICollection<SubjectEntity> Subjects { get; set; } = new List<SubjectEntity>();
}

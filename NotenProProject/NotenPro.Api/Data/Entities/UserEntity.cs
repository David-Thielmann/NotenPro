using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NotenPro.Api.Data.Entities;

public enum UserRole
{
    Student = 0,
    Teacher = 1,
    SchoolAdmin = 2,
    SystemAdmin = 3
}

[Table("users")]
public class UserEntity
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
    [Column("email")]
    [MaxLength(200)]
    public string Email { get; set; } = string.Empty;

    [Column("password_hash")]
    [MaxLength(500)]
    public string? PasswordHash { get; set; }
    
    [Required]
    [Column("role")]
    public UserRole Role { get; set; }

    [Column("school_id")]
    [MaxLength(36)]
    public string? SchoolId { get; set; }

    [Column("is_active")]
    public bool IsActive { get; set; } = true;

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    [Required]
    [Column("external_id")]
    [MaxLength(64)]
    public string ExternalId { get; set; } = string.Empty;


    // Navigation Properties
    [ForeignKey("SchoolId")]
    public SchoolEntity? School { get; set; }

    public ICollection<GradeEntity> Grades { get; set; } = new List<GradeEntity>();
    public ICollection<NotificationEntity> Notifications { get; set; } = new List<NotificationEntity>();
    public ICollection<TestEntity> CreatedTests { get; set; } = new List<TestEntity>();
    public ICollection<StudentClassEntity> StudentClasses { get; set; } = new List<StudentClassEntity>();
    public ICollection<TeacherSubjectEntity> TeacherSubjects { get; set; } = new List<TeacherSubjectEntity>();
    

}




using Microsoft.EntityFrameworkCore;
using NotenPro.Api.Data.Entities;

namespace NotenPro.Api.Data;

public class NotenProDbContext : DbContext
{
    public NotenProDbContext(DbContextOptions<NotenProDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<UserEntity> Users { get; set; } = null!;
    public DbSet<SchoolEntity> Schools { get; set; } = null!;
    public DbSet<ClassEntity> Classes { get; set; } = null!;
    public DbSet<SubjectEntity> Subjects { get; set; } = null!;
    public DbSet<TestEntity> Tests { get; set; } = null!;
    public DbSet<GradeEntity> Grades { get; set; } = null!;
    public DbSet<NotificationEntity> Notifications { get; set; } = null!;
    public DbSet<StudentClassEntity> StudentClasses { get; set; } = null!;
    public DbSet<TeacherSubjectEntity> TeacherSubjects { get; set; } = null!;
    public DbSet<EarlyWarningEntity> EarlyWarnings { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // User Configuration
        modelBuilder.Entity<UserEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Email).IsUnique();
            entity.Property(e => e.Role).HasConversion<int>();
        });

        // School Configuration
        modelBuilder.Entity<SchoolEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.Name);
        });

        // Class Configuration
        modelBuilder.Entity<ClassEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.SchoolId, e.Name });
        });

        // Subject Configuration
        modelBuilder.Entity<SubjectEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.SchoolId, e.Name });
        });

        // Test Configuration
        modelBuilder.Entity<TestEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.ClassId, e.Date });
            entity.Property(e => e.Type).HasConversion<int>();
        });

        // Grade Configuration
        modelBuilder.Entity<GradeEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.StudentId, e.TestId }).IsUnique();
            entity.Property(e => e.Status).HasConversion<int>();
            entity.Property(e => e.GradeValue).HasPrecision(3, 2);
        });

        // Notification Configuration
        modelBuilder.Entity<NotificationEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.UserId, e.Timestamp });
            entity.Property(e => e.Type).HasConversion<int>();
        });

        // StudentClass Configuration
        modelBuilder.Entity<StudentClassEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.StudentId, e.ClassId }).IsUnique();
        });

        // TeacherSubject Configuration
        modelBuilder.Entity<TeacherSubjectEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.TeacherId, e.SubjectId }).IsUnique();
        });

        // EarlyWarning Configuration
        modelBuilder.Entity<EarlyWarningEntity>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => new { e.StudentId, e.SubjectId, e.CreatedAt });
            entity.Property(e => e.CurrentAverage).HasPrecision(3, 2);
        });

        // Seed Data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        // System Admin
        var sysAdminId = Guid.NewGuid().ToString();
        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = sysAdminId,
            Name = "System Administrator",
            Email = "sysadmin@notenpro.at",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = UserRole.SystemAdmin,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // HTL Krems School
        var htlKremsId = Guid.NewGuid().ToString();
        modelBuilder.Entity<SchoolEntity>().HasData(new SchoolEntity
        {
            Id = htlKremsId,
            Name = "HTL Krems",
            Location = "Krems an der Donau",
            Status = "Active",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // School Admin for HTL Krems
        var schoolAdminId = Guid.NewGuid().ToString();
        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = schoolAdminId,
            Name = "HTL Admin",
            Email = "admin@htl-krems.ac.at",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
            Role = UserRole.SchoolAdmin,
            SchoolId = htlKremsId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Sample Classes
        var class5AHITId = Guid.NewGuid().ToString();
        var class5BHITId = Guid.NewGuid().ToString();

        // Sample Teacher
        var teacherId = Guid.NewGuid().ToString();
        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = teacherId,
            Name = "Prof. Maria Schmidt",
            Email = "maria.schmidt@htl-krems.ac.at",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Teacher@123"),
            Role = UserRole.Teacher,
            SchoolId = htlKremsId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        modelBuilder.Entity<ClassEntity>().HasData(
            new ClassEntity
            {
                Id = class5AHITId,
                Name = "5AHIT",
                SchoolId = htlKremsId,
                ClassTeacherId = teacherId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new ClassEntity
            {
                Id = class5BHITId,
                Name = "5BHIT",
                SchoolId = htlKremsId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Sample Subjects
        var mathId = Guid.NewGuid().ToString();
        var germanId = Guid.NewGuid().ToString();
        var englishId = Guid.NewGuid().ToString();
        var programmingId = Guid.NewGuid().ToString();

        modelBuilder.Entity<SubjectEntity>().HasData(
            new SubjectEntity
            {
                Id = mathId,
                Name = "Mathematik",
                Description = "Angewandte Mathematik",
                SchoolId = htlKremsId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubjectEntity
            {
                Id = germanId,
                Name = "Deutsch",
                Description = "Deutsche Sprache und Literatur",
                SchoolId = htlKremsId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubjectEntity
            {
                Id = englishId,
                Name = "Englisch",
                Description = "English Language",
                SchoolId = htlKremsId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            },
            new SubjectEntity
            {
                Id = programmingId,
                Name = "Programmieren",
                Description = "Software Engineering",
                SchoolId = htlKremsId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            }
        );

        // Teacher Subjects
        modelBuilder.Entity<TeacherSubjectEntity>().HasData(
            new TeacherSubjectEntity
            {
                Id = Guid.NewGuid().ToString(),
                TeacherId = teacherId,
                SubjectId = mathId,
                AssignedAt = DateTime.UtcNow
            },
            new TeacherSubjectEntity
            {
                Id = Guid.NewGuid().ToString(),
                TeacherId = teacherId,
                SubjectId = programmingId,
                AssignedAt = DateTime.UtcNow
            }
        );

        // Sample Student
        var studentId = Guid.NewGuid().ToString();
        modelBuilder.Entity<UserEntity>().HasData(new UserEntity
        {
            Id = studentId,
            Name = "Max Mustermann",
            Email = "max.mustermann@students.htl-krems.ac.at",
            PasswordHash = BCrypt.Net.BCrypt.HashPassword("Student@123"),
            Role = UserRole.Student,
            SchoolId = htlKremsId,
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Student Class Assignment
        modelBuilder.Entity<StudentClassEntity>().HasData(new StudentClassEntity
        {
            Id = Guid.NewGuid().ToString(),
            StudentId = studentId,
            ClassId = class5AHITId,
            EnrolledAt = DateTime.UtcNow
        });

        // Sample Test
        var testId = Guid.NewGuid().ToString();
        modelBuilder.Entity<TestEntity>().HasData(new TestEntity
        {
            Id = testId,
            Name = "Algebra Test 1",
            SubjectId = mathId,
            ClassId = class5AHITId,
            TeacherId = teacherId,
            Date = DateTime.UtcNow.AddDays(-7),
            MaxPoints = 100,
            Type = TestType.Test,
            Description = "Lineare Gleichungen und Funktionen",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Sample Grade
        modelBuilder.Entity<GradeEntity>().HasData(new GradeEntity
        {
            Id = Guid.NewGuid().ToString(),
            StudentId = studentId,
            TestId = testId,
            GradeValue = 2.00m,
            Points = 82,
            MaxPoints = 100,
            Status = GradeStatus.Graded,
            Comment = "Sehr gute Leistung!",
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        });

        // Sample Notification
        modelBuilder.Entity<NotificationEntity>().HasData(new NotificationEntity
        {
            Id = Guid.NewGuid().ToString(),
            UserId = studentId,
            Title = "Neue Note verfügbar",
            Message = "Deine Note für 'Algebra Test 1' wurde eingetragen: 2.00 (Gut)",
            Type = NotificationType.Success,
            IsRead = false,
            Timestamp = DateTime.UtcNow
        });
    }
}
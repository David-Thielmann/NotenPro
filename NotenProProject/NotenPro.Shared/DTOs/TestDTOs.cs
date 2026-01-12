namespace NotenPro.Api.DTOs;

public class TestDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SubjectId { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string TeacherId { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int MaxPoints { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int GradedCount { get; set; }
    public int TotalStudents { get; set; }
}

public class CreateTestRequest
{
    public string Name { get; set; } = string.Empty;
    public string SubjectId { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int MaxPoints { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class UpdateTestRequest
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int MaxPoints { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
}

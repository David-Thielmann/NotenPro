namespace NotenPro.Api.DTOs;

public class GradeDto
{
    public string Id { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string TestId { get; set; } = string.Empty;
    public string TestName { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public decimal? GradeValue { get; set; }
    public int? Points { get; set; }
    public int? MaxPoints { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
    public DateTime Date { get; set; }
}

public class CreateGradeRequest
{
    public string StudentId { get; set; } = string.Empty;
    public string TestId { get; set; } = string.Empty;
    public decimal? GradeValue { get; set; }
    public int? Points { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Comment { get; set; }
}

public class UpdateGradeRequest
{
    public decimal? GradeValue { get; set; }
    public int? Points { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? Comment { get; set; }
}

public class BulkGradeRequest
{
    public string TestId { get; set; } = string.Empty;
    public List<StudentGradeInput> Grades { get; set; } = new();
}

public class StudentGradeInput
{
    public string StudentId { get; set; } = string.Empty;
    public decimal? GradeValue { get; set; }
    public int? Points { get; set; }
    public string Status { get; set; } = "Pending";
    public string? Comment { get; set; }
}

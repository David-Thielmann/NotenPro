namespace NotenPro.Shared.DTOs;

public sealed class ClassOverviewDto
{
    public string ClassId { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public string? PrimarySubject { get; set; }
    public int StudentCount { get; set; }
    public double AverageGrade { get; set; }
    public List<int> GradeDistribution { get; set; } = new();
    public List<StudentAverageDto> Students { get; set; } = new();
}

public sealed class StudentAverageDto
{
    public string StudentId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public double Average { get; set; }
}

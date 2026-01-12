namespace NotenPro.Api.DTOs;

public class SchoolDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int TeacherCount { get; set; }
    public int StudentCount { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateSchoolRequest
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
}

public class UpdateSchoolRequest
{
    public string Name { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}

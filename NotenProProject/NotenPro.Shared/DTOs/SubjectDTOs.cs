namespace NotenPro.Shared.DTOs;

public class SubjectDto
{
    public string? Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
    public string SchoolId { get; set; } = string.Empty;
}

public class CreateSubjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string SchoolId { get; set; } = string.Empty;
}

public class UpdateSubjectRequest
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}

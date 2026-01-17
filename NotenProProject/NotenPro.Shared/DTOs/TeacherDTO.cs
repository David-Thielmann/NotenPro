namespace NotenPro.Shared.DTOs;

public class TeacherDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
    public string SubjectName { get; set; } = "Kein Fach";
    
    // Das ist wichtig für das MudSelect!
    public string? PrimarySubjectId { get; set; }
}

public class CreateTeacherRequest
{
    public TeacherDto Teacher { get; set; } = new();
    public string Password { get; set; } = string.Empty;
}
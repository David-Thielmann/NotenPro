namespace NotenPro.Shared.DTOs;

public class EarlyWarningDto
{
    public string Id { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string SubjectId { get; set; } = string.Empty;
    public string Subject { get; set; } = string.Empty;
    public string TeacherId { get; set; } = string.Empty;
    public string TeacherName { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public decimal CurrentAverage { get; set; }
    public bool IsSent { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime CreatedAt { get; set; }
}

public class CreateEarlyWarningRequest
{
    public string StudentId { get; set; } = string.Empty;
    public string SubjectId { get; set; } = string.Empty;
    public string Reason { get; set; } = string.Empty;
    public decimal CurrentAverage { get; set; }
}

public class SendEarlyWarningsRequest
{
    public List<string> WarningIds { get; set; } = new();
}

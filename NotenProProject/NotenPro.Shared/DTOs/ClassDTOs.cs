namespace NotenPro.Shared.DTOs;


public class ClassDto
{
    public string Id { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string SchoolId { get; set; } = string.Empty;
    
    // Wir nutzen NUR diese beiden für die Lehrer-Logik im UI:
    public string? TeacherId { get; set; } 
    public string TeacherName { get; set; } = "Nicht zugewiesen";
    
    public int StudentCount { get; set; }
    public decimal AverageGrade { get; set; }
}

// ... CreateClassRequest und UpdateClassRequest können so bleiben wie sie sind
public class CreateClassRequest
{
    public string Name { get; set; } = string.Empty;
    public string SchoolId { get; set; } = string.Empty;
    public string? ClassTeacherId { get; set; }
}

public class UpdateClassRequest
{
    public string Name { get; set; } = string.Empty;
    public string? ClassTeacherId { get; set; }
}

public class StudentClassDto
{
    public string Id { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public string StudentName { get; set; } = string.Empty;
    public string ClassId { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public DateTime EnrolledAt { get; set; }
}

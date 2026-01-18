namespace NotenPro.Shared.DTOs
{
    public class TestDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public string Type { get; set; } = "Test"; // z.B. Schularbeit, Test, Mitarbeit
        public int MaxPoints { get; set; }
        public double Weighting { get; set; } = 1.0;
        
        // Hilfsfelder für die Anzeige in der Lehrer-Tabelle
        public string ClassId { get; set; } = string.Empty;
        public string ClassName { get; set; } = string.Empty;
        public string SubjectId { get; set; } = string.Empty;
        public string SubjectName { get; set; } = string.Empty;
    }
}

namespace NotenPro.Shared.DTOs
{
    public class CreateTestRequest
    {
        public string Name { get; set; } = string.Empty;
        public string ClassId { get; set; } = string.Empty;
        public string SubjectId { get; set; } = string.Empty;
        public DateTime Date { get; set; } = DateTime.Today;
        public string Type { get; set; } = "Test";
        public int MaxPoints { get; set; } = 100;
        
    }
}

public class UpdateTestRequest
{
    public string Name { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int MaxPoints { get; set; }
    public string Type { get; set; } = string.Empty;
    public string? Description { get; set; }
}

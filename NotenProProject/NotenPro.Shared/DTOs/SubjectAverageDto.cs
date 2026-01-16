namespace NotenPro.Shared.DTOs;

public class SubjectAverageDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Average { get; set; }
    public int TestCount { get; set; }
}
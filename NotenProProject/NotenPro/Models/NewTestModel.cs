using System;
using System.ComponentModel.DataAnnotations;

public class NewTestModel
{
    [Required]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Subject { get; set; } = string.Empty;

    [Required]
    public DateTime? Date { get; set; } 

    [Range(1, 1000)]
    public int MaxPoints { get; set; } = 100;

    [Range(1, 100)]
    public int WeightPercentage { get; set; } = 100;
}
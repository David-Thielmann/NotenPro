namespace NotenPro.Shared.DTOs;

public sealed class AuthMeDto
{
    public string Id { get; set; } = string.Empty;
    public string ExternalId { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
    public string? SchoolId { get; set; }
    public string? SchoolName { get; set; }
}

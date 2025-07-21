using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record UniversityForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; init; }
    public string? Abbreviation { get; init; }

    [Required(ErrorMessage = "Type is a required field.")]
    public string? Type { get; init; }
    public string? City { get; init; }
    public string? Address { get; init; }

    [Required(ErrorMessage = "Email is a required field.")]
    [EmailAddress]
    public string? Email { get; init; }
    [Phone]
    public string? PhoneNumber { get; init; }
    public int? YearEstablished { get; init; }
    public string? LogoUrl { get; init; }
    public string? Description { get; init; }
}

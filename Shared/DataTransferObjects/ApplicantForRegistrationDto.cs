
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicantForRegistrationDto
{
    [Required(ErrorMessage = "FirstName is required")]
    [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is 50 characters.")]
    public string? FirstName { get; init; }

    [Required(ErrorMessage = "LastName is required")]
    [MaxLength(50, ErrorMessage = "Maximum length for the LastName is 50 characters.")]
    public string? LastName { get; init; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; init; }

    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; init; }
}


using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public abstract record FacultyForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Code is a required field.")]
    public string? Code { get; init; }

    [Required(ErrorMessage = "Type is a required field.")]
    public string? Type { get; init; }

    [Required(ErrorMessage = "Region is a required field.")]
    public string? Region { get; init; }

    [Required(ErrorMessage = "City is a required field.")]
    public string? City { get; init; }

    [Required(ErrorMessage = "Address is a required field.")]
    public string? Address { get; init; }

    [Required(ErrorMessage = "Email is a required field.")]
    [EmailAddress]
    public string? Email { get; init; }

    [Phone]
    public string? PhoneNumber { get; init; }
    public int? EstablishedYear { get; init; }
}

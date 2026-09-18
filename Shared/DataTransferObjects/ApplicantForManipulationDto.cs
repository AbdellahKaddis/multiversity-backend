
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicantForManipulationDto
{

    [Required(ErrorMessage = "DOB is a required field.")]
    public DateTime? DOB { get; init; }

    [Required(ErrorMessage = "PlaceOfBirth is a required field.")]
    public string? PlaceOfBirth { get; init; }

    [Required(ErrorMessage = "Gender is a required field.")]
    public string? Gender { get; init; }

    [Required(ErrorMessage = "CIN is a required field.")]
    public string? CIN { get; init; }

    [Required(ErrorMessage = "MassarCode is a required field.")]
    public string? MassarCode { get; init; }

    [Required(ErrorMessage = "Phone is a required field.")]
    public string? Phone { get; init; }

    [Required(ErrorMessage = "PhotoUrl is a required field.")]
    public string? PhotoUrl { get; init; }

    [Required(ErrorMessage = "Status is a required field.")]
    public string? Status { get; init; }

    [Required(ErrorMessage = "Faculty ID is a required field.")]
    public Guid? FacultyId { get; init; }
}

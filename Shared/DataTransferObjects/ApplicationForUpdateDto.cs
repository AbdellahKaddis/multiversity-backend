

using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicationForUpdateDto : ApplicationForManipulationDto
{
    [Required(ErrorMessage = "Applicant is a required field.")]
    public ApplicantForUpdateDto? Applicant { get; init; }

    [Required(ErrorMessage = "Status is a required field.")]
    public string? Status { get; init; }
}

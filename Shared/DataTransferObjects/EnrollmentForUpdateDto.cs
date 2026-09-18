
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record EnrollmentForUpdateDto : EnrollmentForManipulationDto
{
    [Required(ErrorMessage = "Status is a required field.")]
    public string? Status { get; init; }
}

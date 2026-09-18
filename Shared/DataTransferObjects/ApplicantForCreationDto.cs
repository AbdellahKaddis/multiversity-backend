
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicantForCreationDto : ApplicantForManipulationDto
{
    [Required(ErrorMessage = "Id is a required field.")]
    public string? Id { get; init; }
}

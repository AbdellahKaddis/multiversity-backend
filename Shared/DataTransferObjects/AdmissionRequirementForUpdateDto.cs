
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionRequirementForUpdateDto : AdmissionRequirementForManipulationDto
{
    [Required(ErrorMessage = "AdmissionId is a required field.")]
    public Guid AdmissionId { get; init; }
    public Guid Id { get; init; }
}




using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionForCreationDto : AdmissionForManipulationDto
{
    [Required(ErrorMessage = "Admission Requirements is a required field.")]
    public ICollection<AdmissionRequirementForCreationDto> Requirements { get; init; } = new List<AdmissionRequirementForCreationDto>();
}

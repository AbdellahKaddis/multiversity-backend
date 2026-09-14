

using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionForUpdateDto : AdmissionForManipulationDto {
    [Required(ErrorMessage = "Admission Requirements is a required field.")]
    public ICollection<AdmissionRequirementForUpdateDto> Requirements { get; init; } = new List<AdmissionRequirementForUpdateDto>();
}



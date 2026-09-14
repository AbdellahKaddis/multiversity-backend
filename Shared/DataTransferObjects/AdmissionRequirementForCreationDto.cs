
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionRequirementForCreationDto : AdmissionRequirementForManipulationDto
{
    public Guid? AdmissionId { get; init; }
}

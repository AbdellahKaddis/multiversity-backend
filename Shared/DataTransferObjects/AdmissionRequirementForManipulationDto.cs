
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionRequirementForManipulationDto
{

    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "DisplayOrder is a required field.")]
    [Range(1, int.MaxValue)]
    public ushort DisplayOrder { get; init; }
}

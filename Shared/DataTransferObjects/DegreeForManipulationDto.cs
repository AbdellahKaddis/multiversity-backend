using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record DegreeForManipulationDto
{
    [Required(ErrorMessage = "Degree name is a required field.")]
    public string? Name { get; init; }
}



using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record FacultyDeanForManipulationDto
{
    [Required(ErrorMessage = "DeanId is a required field.")]
    public string? DeanId { get; init; }

    [Required(ErrorMessage = "FacultyId is a required field.")]
    public Guid? FacultyId { get; init; }
}

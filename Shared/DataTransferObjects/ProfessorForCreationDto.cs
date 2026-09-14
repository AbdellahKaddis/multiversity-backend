using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ProfessorForCreationDto : ProfessorForManipulationDto
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress]
    public string? Email { get; init; }
}

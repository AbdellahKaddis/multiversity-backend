using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record DepartmentForManipulationDto
{
    [Required(ErrorMessage = "Depatment name is a required field."), MaxLength(100)]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Depatment Code is a required field."), MaxLength(10)]
    public string? Code { get; init; }

    [MaxLength(500)]
    public string? Description { get; init; }

    [EmailAddress, MaxLength(150)]
    public string? Email { get; init; }
}

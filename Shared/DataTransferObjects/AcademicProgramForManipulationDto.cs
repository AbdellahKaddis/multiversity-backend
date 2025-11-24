using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public abstract record AcademicProgramForManipulationDto
{
    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Code is a required field."), MaxLength(10)]
    public string? Code { get; init; }

    [Required(ErrorMessage = "DurationInYears is a required field.")]
    public int DurationInYears { get; init; }
    public string? Description { get; init; }

    [Required(ErrorMessage = "DegreeId is a required field.")]
    public Guid DegreeId { get; init; }

}

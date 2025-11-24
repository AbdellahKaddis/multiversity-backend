using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record ProgramCourseForManipulationDto
{

    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid CourseId { get; init; }

    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid ProgramId { get; init; }

    [Required(ErrorMessage = "Semester is a required field.")]
    [Range(1, 6, ErrorMessage = "Semester must be between 1 and 6.")]
    public uint Semester { get; init; }
}

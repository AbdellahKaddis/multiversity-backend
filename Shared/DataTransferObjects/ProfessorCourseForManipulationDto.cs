
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ProfessorCourseForManipulationDto
{

    [Required(ErrorMessage = "ProfessorId is a required field.")]
    public string? ProfessorId { get; init; }

    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid CourseId { get; init; }

    [Required(ErrorMessage = "TeachingType is a required field.")]
    public string? TeachingType { get; init; }

    [Required(ErrorMessage = "AcademicYear is a required field.")]
    public string? AcademicYear { get; init; }
}

using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record GradeForCreationDto : GradeForManipulationDto
{
    [Required(ErrorMessage = "EnrollmentId is a required field.")]
    public Guid? EnrollmentId { get; init; }

    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid? CourseId { get; init; }
}
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record PublishGradesDto
{
    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid? CourseId { get; init; }

    [Required(ErrorMessage = "AcademicYear is a required field."), MaxLength(9)]
    public string? AcademicYear { get; init; }

    [Required(ErrorMessage = "Semester is a required field."), MaxLength(10)]
    public string? Semester { get; init; }

    [Required(ErrorMessage = "Session is a required field."), MaxLength(20)]
    public string? Session { get; init; }
}
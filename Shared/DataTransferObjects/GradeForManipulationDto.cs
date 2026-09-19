using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public abstract record GradeForManipulationDto
{
    [Required(ErrorMessage = "AcademicYear is a required field."), MaxLength(9)]
    public string? AcademicYear { get; init; }

    [Required(ErrorMessage = "Semester is a required field."), MaxLength(10)]
    public string? Semester { get; init; }

    [Required(ErrorMessage = "Session is a required field."), MaxLength(20)]
    public string? Session { get; init; }

    [Range(0, 20, ErrorMessage = "Score must be between 0 and 20.")]
    public decimal? Score { get; init; }

    public string? ProfessorId { get; init; }
}
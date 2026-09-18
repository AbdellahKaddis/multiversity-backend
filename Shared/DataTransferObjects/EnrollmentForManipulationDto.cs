
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record EnrollmentForManipulationDto
{
 

    [Required(ErrorMessage = "ApplicantId is a required field.")]
    public string? ApplicantId { get; init; }

    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid? ProgramId { get; init; }

    [Required(ErrorMessage = "FacultyId is a required field.")]
    public Guid? FacultyId { get; init; }

    [Required(ErrorMessage = "AcademicYear is a required field."), MaxLength(9)]
    public string? AcademicYear { get; init; }


    [Required(ErrorMessage = "YearLevel is a required field.")]
    [Range(1,int.MaxValue)]
    public int? YearLevel { get; init; }
}

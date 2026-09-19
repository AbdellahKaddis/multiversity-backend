
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Enrollment
{
    [Column("EnrollmentId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "ApplicantId is a required field.")]
    public string? ApplicantId { get; set; } 
    public Applicant Applicant { get; set; }

    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid? ProgramId { get; set; }
    public AcademicProgram Program { get; set; }

    [Required(ErrorMessage = "FacultyId is a required field.")]
    public Guid? FacultyId { get; set; }
    public Faculty Faculty { get; set; } 

    [Required(ErrorMessage = "AcademicYear is a required field."), MaxLength(9)]
    public string? AcademicYear { get; set; } 

    [Required(ErrorMessage = "StudentNumber is a required field."), MaxLength(20)]
    public string? StudentNumber { get; set; } 

    [Required(ErrorMessage = "Status is a required field.")]
    public string? Status { get; set; }

    [Required(ErrorMessage = "YearLevel is a required field.")]
    [Range(1, int.MaxValue)]
    public int? YearLevel { get; set; }
    public DateTime EnrolledAt { get; set; } = DateTime.UtcNow;
    public ICollection<Grade>? Grades { get; set; }
}

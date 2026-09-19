
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Entities.Models;

public class Grade
{
    [Column("GradeId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "EnrollmentId is a required field.")]
    public Guid? EnrollmentId { get; set; }
    public Enrollment Enrollment { get; set; } = null!;

    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid? CourseId { get; set; }
    public Course Course { get; set; } = null!;

    [Required(ErrorMessage = "AcademicYear is a required field."), MaxLength(9)]
    public string? AcademicYear { get; set; } = null!;     

    [Required(ErrorMessage = "Semester is a required field."), MaxLength(10)]
    public string? Semester { get; set; } = null!;     

    [Required(ErrorMessage = "Session is a required field."),MaxLength(20)]
    public string? Session { get; set; }

    [Required(ErrorMessage = "Validated is a required field.")]
    public bool? Validated { get; set; }      
    
    public bool? IsPublished { get; set; }

    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 20)]
    public decimal? Score { get; set; }
    public string? ProfessorId { get; set; }
    public Professor Professor { get; set; } = null!;
    public DateTime GradedAt { get; set; } = DateTime.UtcNow;

}

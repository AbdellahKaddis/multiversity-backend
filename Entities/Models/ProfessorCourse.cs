using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class ProfessorCourse
{
    [Column("ProfessorCourseId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "ProfessorId is a required field.")]
    public string? ProfessorId { get; set; }
    public Professor Professor { get; set; }

    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    [Required(ErrorMessage = "TeachingType is a required field.")]
    public string? TeachingType { get; set; }

    [Required(ErrorMessage = "AcademicYear is a required field.")]
    public string? AcademicYear { get; set; }
}
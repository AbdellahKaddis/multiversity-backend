using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Professor
{
    [Column("ProfessorId")]
    public string Id { get; set; }
    public User User { get; set; }

    [MaxLength(50, ErrorMessage = "Maximum length for the Grade is 50 characters.")]
    public string? Grade { get; set; }

    public bool? IsDepartmentHead { get; set; }=false;
    public Department Department { get; set; }

    [Required(ErrorMessage = "DepartmentId is a required field.")]
    public Guid? DepartmentId { get; set; }

    [Required(ErrorMessage = "FacultyId is a required field.")]
    public Guid FacultyId { get; set; }

    public Faculty Faculty { get; set; }
    public ICollection<ProfessorCourse>? ProfessorCourses { get; set; }
    public ICollection<Grade>? Grades { get; set; }
}

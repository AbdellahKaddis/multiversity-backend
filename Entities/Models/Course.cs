using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class Course
{
    [Column("CourseId")]
    public Guid Id { get; set; }
    
    [Required(ErrorMessage = "Code is a required field."), MaxLength(10)]
    public string? Code { get; set; }

    [Required(ErrorMessage = "Code is a required field."), MaxLength(150)]
    public string? Title { get; set; }

    [MaxLength(1000)]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Coefficient is a required field.")]
    [Range(1, int.MaxValue)]
    public uint Coefficient { get; set; }

    [Required(ErrorMessage = "Credits is a required field.")]
    [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10.")]
    public uint Credits { get; set; }

    public uint? HoursCM { get; set; }  
    public uint? HoursTD { get; set; }  
    public uint? HoursTP { get; set; }


    // If a course has a main responsible professor
    //public Guid? ProfessorId { get; set; }
    //public Professor? Professor { get; set; }

    // Many-to-many with Programs
    [Required(ErrorMessage = "Faculty ID is a required field.")]
    public Guid FacultyId { get; set; }
    public Faculty Faculty {  get; set; }
    public ICollection<ProgramCourse>? ProgramCourses { get; set; }
    public ICollection<ProfessorCourse>? ProfessorCourses { get; set; }
}

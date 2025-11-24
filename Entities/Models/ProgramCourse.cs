using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class ProgramCourse
{
    [Column("ProgramCourseId")]
    public Guid Id {  get; set; }

    [Required(ErrorMessage = "CourseId is a required field.")]
    public Guid CourseId { get; set; }
    public Course Course { get; set; }

    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid ProgramId { get; set; }
    public AcademicProgram AcademicProgram { get; set; }

    [Required(ErrorMessage = "Semester is a required field.")]
    [Range(1, 6, ErrorMessage = "Semester nust be between 1 and 6.")]
    public uint Semester { get; set; }

}

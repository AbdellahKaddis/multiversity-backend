using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class AcademicProgram
{
    [Column("ProgramId")]
    public Guid Id {  get; set; }

    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Code is a required field."), MaxLength(10)]
    public string? Code { get; set; }

    [Required(ErrorMessage = "DurationInYears is a required field.")]
    [Range(1, int.MaxValue)]
    public uint DurationInYears { get; set; }
    public string? Description { get; set; }

    [Required(ErrorMessage = "DepartmentId is a required field.")]
    public Guid DepartmentId { get; set; }
    public Department Department { get; set; }

    [Required(ErrorMessage = "DegreeId is a required field.")]
    public Guid DegreeId {  get; set; }
    public Degree Degree {  get; set; }
    public ICollection<ProgramCourse> ProgramCourses { get; set; }

    public ICollection<Admission> Admissions { get; set; }

    //public string? CoordinatorId { get; set; }

    //public ApplicationUser? Coordinator { get; set; }

    //public ICollection<Student> Students { get; set; }
}

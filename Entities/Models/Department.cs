using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class Department
{
    [Column("DepartmentId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Depatment name is a required field."), MaxLength(100)]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Depatment Code is a required field."), MaxLength(10)]
    public string? Code { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    [EmailAddress, MaxLength(150)]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Faculty Id is a required field.")]
    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public ICollection<AcademicProgram>? Programs { get; set; }
    public ICollection<Professor>? Professors { get; set; }

}

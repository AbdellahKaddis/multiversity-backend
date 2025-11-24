using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class Degree
{
    [Column("DegreeId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Degree name is a required field.")]
    public string? Name {  get; set; }

    [Required(ErrorMessage = "UniversityId is a required field.")]
    public Guid UniversityId {  get; set; }
    public University University {  get; set; }
    public ICollection<AcademicProgram>? Programs { get; set; }
}

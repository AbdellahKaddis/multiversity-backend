using Entities.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Admission
{
    [Column("AdmissionId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid ProgramId { get; set; }
    public AcademicProgram Program { get; set; }

    [Required(ErrorMessage = "Title is a required field.")]
    public string? Title { get; set; }

    [Required(ErrorMessage = "AcademicYear is a required field.")]
    public string? AcademicYear { get; set; }

    [Required(ErrorMessage = "StartDate is a required field.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "EndDate is a required field.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "Process is a required field.")]
    public string? Process { get; set; }
    public ICollection<AdmissionRequirement> Requirements { get; set; }
}
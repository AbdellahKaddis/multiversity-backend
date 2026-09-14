
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionForManipulationDto
{
    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid ProgramId { get; set; }
    [Required(ErrorMessage = "Title is a required field.")]
    public string? Title { get; init; }

    [Required(ErrorMessage = "AcademicYear is a required field.")]
    public string? AcademicYear { get; init; }

    [Required(ErrorMessage = "StartDate is a required field.")]
    public DateTime? StartDate { get; init; }

    [Required(ErrorMessage = "EndDate is a required field.")]
    public DateTime? EndDate { get; init; }

    [Required(ErrorMessage = "Process is a required field.")]
    public string? Process { get; init; }
}


using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ProfessorForManipulationDto
{
    [Required(ErrorMessage = "FirstName is required")]
    [MaxLength(50, ErrorMessage = "Maximum length for the FirstName is 50 characters.")]
    public string? FirstName { get; init; }

    [Required(ErrorMessage = "LastName is required")]
    [MaxLength(50, ErrorMessage = "Maximum length for the LastName is 50 characters.")]
    public string? LastName { get; init; }

   
    [MaxLength(10, ErrorMessage = "Maximum length for the Cin is 10 characters.")]
    public string? Cin { get; set; }

    [MaxLength(50, ErrorMessage = "Maximum length for the Grade is 50 characters.")]
    public string? Grade { get; set; }

    public bool? IsDepartmentHead { get; set; }
    [Required(ErrorMessage = "FacultyId is a required field.")]
    public Guid FacultyId { get; set; }
    [Required(ErrorMessage = "DepartmentId is a required field.")]
    public Guid DepartmentId { get; set; }
}

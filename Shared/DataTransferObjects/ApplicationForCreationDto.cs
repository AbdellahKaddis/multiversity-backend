using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicationForCreationDto : ApplicationForManipulationDto
{
    [Required(ErrorMessage ="Applicant is a required field.")]
    public ApplicantForUpdateDto? Applicant { get; init; }    
}


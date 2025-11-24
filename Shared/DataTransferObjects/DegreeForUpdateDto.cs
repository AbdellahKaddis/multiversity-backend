using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record DegreeForUpdateDto(
    [Required(ErrorMessage = "UniversityId is a required field.")]
  Guid UniversityId) : DegreeForManipulationDto;

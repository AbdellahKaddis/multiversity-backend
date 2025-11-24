using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record AcademicProgramForUpdateDto(
    [Required(ErrorMessage = "DepartmentId is a required field.")]
  Guid DepartmentId) :AcademicProgramForManipulationDto;

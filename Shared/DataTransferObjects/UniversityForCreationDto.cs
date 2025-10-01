using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record UniversityForCreationDto([Required(ErrorMessage = "Admin Id is a required field.")]string? AdminId ) : UniversityForManipulationDto;


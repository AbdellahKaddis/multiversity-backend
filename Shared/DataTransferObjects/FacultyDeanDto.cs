using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record FacultyDeanDto(
    Guid Id,
    string? DeanId,
    Guid FacultyId,
    DateTime StartDate,
    DateTime? EndDate,
    FacultyDto Faculty,
    string DeanName);

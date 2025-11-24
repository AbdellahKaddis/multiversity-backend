using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record FacultyDto(
       Guid Id,
       string? Name,
       string? Code,
       string? Type,
       string? Region,
       string? City,
       string? Address, 
       string? Email, 
       string? PhoneNumber,
       int? EstablishedYear, 
       Guid UniversityId,
       string? DeanId,
       string? DeanName
    );

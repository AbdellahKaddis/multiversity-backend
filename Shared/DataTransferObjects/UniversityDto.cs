using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Shared.DataTransferObjects;

public record UniversityDto
(Guid Id,
    string? Name,
    string? Abbreviation,
    string? Type,
    string? City,
    string? Address,
    string? Email,
    string? PhoneNumber,
    int? YearEstablished,
    string? LogoUrl,
    string? Description,
    string? AdminId);



using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicantDto
(
string? Id,
string? FullName,
string? StudentNumber,
DateTime? DOB,
string? PlaceOfBirth,
string? Gender,
string? CIN,
string? MassarCode,
string? Phone,
string? PhotoUrl,
string? Status,
DateTime CreatedAt,
DateTime EnrolledAt,
Guid FacultyId
);

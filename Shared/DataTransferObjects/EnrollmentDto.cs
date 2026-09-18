namespace Shared.DataTransferObjects;

public record EnrollmentDto(
    Guid Id,
    string? ApplicantId,
    string? ApplicantFullName,
    Guid? ProgramId,
    string? ProgramName,
    Guid? FacultyId,
    string? FacultyName,
    string? AcademicYear,
    string? StudentNumber,
    string? Status,
    int? YearLevel,
    DateTime EnrolledAt
);
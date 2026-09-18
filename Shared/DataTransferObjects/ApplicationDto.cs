
namespace Shared.DataTransferObjects;

public record ApplicationDto(
    Guid Id,
    string? ApplicantId,
    string? ApplicantFullName,
    Guid? ProgramId,
    string? ProgramName,
    string? FacultyName,
    decimal? Grade,
    string? BacSerie,
    uint? BacYear,
    string? BacMention,
    string? Status,
    DateTime SubmittedAt,
    string? ReviewerId,
    string? ReviewerName,
    string? FileUrl,
    ApplicantDto Applicant
);

namespace Shared.DataTransferObjects;
public record AcademicProgramDto(
    Guid Id,
    string Name,
    string Code,
    int DurationInYears,
    string? Description,
    Guid DepartmentId,
    Guid DegreeId,
    string DepartmentName,
    string DegreeName,
      string? FacultyName,
    Guid? FacultyId
    );

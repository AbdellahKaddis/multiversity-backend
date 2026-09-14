using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ProfessorDto(
    string Id,
    string FirstName,
    string LastName,
    string Email,
    string Cin,
    string Grade,
    bool IsDepartmentHead,
    Guid FacultyId,
    Guid DepartmentId,
    FacultyDto Faculty,
    string DepartmentName);

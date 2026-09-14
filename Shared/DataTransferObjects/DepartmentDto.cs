namespace Shared.DataTransferObjects;

public record DepartmentDto(Guid Id, string Name, string Code, string? Description, string? email, Guid FacultyId, string? DepartmentHead);

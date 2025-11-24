using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IDepartmentService
{
    Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync
        (Guid facultyId, bool trackChanges);
    Task<DepartmentDto> GetDepartmentAsync(Guid facultyId, Guid id, bool trackChanges);
    Task<DepartmentDto> CreateDepartmentForFacultyAsync(Guid facultyId, DepartmentForCreationDto
    departmentForCreation, bool trackChanges);
    Task DeleteDepartmentForFacultyAsync(Guid facultyId, Guid id, bool trackChanges);
    Task UpdateDepartmentForFacultyAsync(Guid facultyId, Guid id,
    DepartmentForUpdateDto departmentForUpdate, bool facTrackChanges, bool
    depTrackChanges);
}

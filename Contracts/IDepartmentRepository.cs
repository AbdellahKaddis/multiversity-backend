using Entities.Models;

namespace Contracts;

public interface IDepartmentRepository
{
    Task<IEnumerable<Department>> GetDepartmentsAsync(Guid facultyId, bool trackChanges);
    Task<Department> GetDepartmentAsync(Guid facultyId, Guid id, bool trackChanges);
    Task<Department> GetDepartmentAsync(Guid? id, bool trackChanges);
    void CreateDepartmentForFaculty(Guid facultyId, Department department);
    void DeleteDepartment(Department department);
}

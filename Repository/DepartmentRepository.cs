using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class DepartmentRepository : RepositoryBase<Department>, IDepartmentRepository
{
    public DepartmentRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateDepartmentForFaculty(Guid facultyId, Department department)
    {
        department.FacultyId = facultyId;
        Create(department);
    }

    public void DeleteDepartment(Department department)
    {
        Delete(department);
    }

    public async Task<Department> GetDepartmentAsync(Guid facultyId, Guid id, bool trackChanges)
    {
        return await FindByCondition(d => d.FacultyId.Equals(facultyId) && d.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
    public async Task<Department> GetDepartmentAsync(Guid? id, bool trackChanges)
    {
        return await FindByCondition(d => d.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Department>> GetDepartmentsAsync(Guid facultyId, bool trackChanges)
    {
        return await FindByCondition(d => d.FacultyId.Equals(facultyId), trackChanges)
            .ToListAsync();
    }
}

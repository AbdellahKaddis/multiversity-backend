using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;
public class AcademicProgramRepository : RepositoryBase<AcademicProgram>, IAcademicProgramRepository
{
    public AcademicProgramRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateProgramForDepartment(Guid departmentId, AcademicProgram program)
    {
        program.DepartmentId = departmentId;
        Create(program);
    }

    public void DeleteProgram(AcademicProgram program)
    {
        Delete(program);
    }

    public async Task<AcademicProgram> GetProgramAsync(Guid departmentId, Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id) && p.DepartmentId.Equals(departmentId), trackChanges)
            .Include(p => p.Degree)
            .Include(p => p.Department)
            .SingleOrDefaultAsync();
    }
    public async Task<AcademicProgram> GetProgramAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id), trackChanges)
            .Include(p => p.Degree)
            .Include(p => p.Department)
            .ThenInclude(d => d.Faculty)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<AcademicProgram>> GetProgramsAsync(ProgramParameters programParameters, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(p => p.Degree)
            .Include(p => p.Department)
            .ThenInclude(d => d.Faculty)
            .FilterPrograms(programParameters.FacultyId, programParameters.DepartmentId, programParameters.UniversityId)
            .ToListAsync();
    }
    public async Task<int> GetCountByUniversityAsync(Guid universityId) { return await FindByCondition(p => p.Department.Faculty.UniversityId == universityId, false).CountAsync(); }
}

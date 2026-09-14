using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.RequestFeatures;

namespace Repository;
public class ProfessorRepository : RepositoryBase<Professor>, IProfessorRepository
{
    public ProfessorRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) { }
    public void CreateProfessor(string? userId, Professor professor)
    {
        professor.Id = userId;
        Create(professor);
    }

    public void DeleteProfessor(Professor professor)
    {
        Delete(professor);
    }

    public async Task<Professor> GetProfessorAsync(string id, bool trackChanges)
    {
        return await FindByCondition(p => p.Id.Equals(id), trackChanges)
            .Include(p => p.User)
            .Include(p => p.Faculty)
            .Include(p => p.Department)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Professor>> GetProfessorsAsync(ProfessorParameteres professorParameteres, bool trackChanges)
    {
        return await FindByCondition(p => p.FacultyId.Equals(professorParameteres.FacultyId) && p.User.DeletedAt == null, trackChanges)
            .Include(p => p.User)
            .Include(p => p.Department)
            .ToListAsync();
    }

    public async Task<bool> IsDepartmentHasHeadAsync(Guid facultyId,Guid departmentId, bool trackChanges)
    {
        return await FindByCondition(p => p.FacultyId.Equals(facultyId), trackChanges)
            .AnyAsync(p => p.DepartmentId == departmentId &&
                   (bool)p.IsDepartmentHead);
            
    }
    public async Task<int> GetCountByUniversityAsync(Guid universityId) { return await FindByCondition(p => p.Faculty.UniversityId == universityId && (bool)p.User.IsActive, false).CountAsync(); }
}

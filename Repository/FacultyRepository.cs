using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class FacultyRepository : RepositoryBase<Faculty>, IFacultyRepository
{
    public FacultyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
    {
    }

    public void CreateFacultyForUniversity(Guid universityId, Faculty faculty)
    {
        faculty.UniversityId = universityId;
        Create(faculty);
    }

    public void DeleteFaculty(Faculty faculty)
    {
        Delete(faculty);
    }

    public async Task<IEnumerable<Faculty>> GetFacultiesAsync(Guid universityId, bool trackChanges)
    {
        return await FindByCondition(f => f.UniversityId.Equals(universityId),
            trackChanges)
            .ToListAsync();
    }

    public async Task<Faculty> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges)
    {
        return await FindByCondition(f => f.UniversityId.Equals(universityId) && f.Id.Equals(id),
            trackChanges)
            .SingleOrDefaultAsync();
    }
}

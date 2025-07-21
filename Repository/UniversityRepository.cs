using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class UniversityRepository : RepositoryBase<University>, IUniversityRepository
{
    public UniversityRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
    }
    public void CreateUniversity(University university)
    {
        Create(university);
    }

    public void DeleteUniversity(University university)
    {
        Delete(university);
    }

    public async Task<IEnumerable<University>> GetAllUniversitiesAsync(bool trackChanges)
    {
        return await FindAll(trackChanges)
            .OrderByDescending(u => u.Id)
            .ToListAsync();
    }

    public async Task<University> GetUniversityAsync(Guid univeristyId, bool trackChanges)
    {
        return await FindByCondition(u => u.Id.Equals(univeristyId), trackChanges)
            .SingleOrDefaultAsync();
    }
}

using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Repository;
public class DegreeRepository : RepositoryBase<Degree>, IDegreeRepository
{
    public DegreeRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateDegree(Guid universityId, Degree degree)
    {
        degree.UniversityId = universityId;
        Create(degree);
    }

    public void DeleteDegree(Degree degree)
    {
        Delete(degree);
    }

    public async Task<IEnumerable<Degree>> GetAllDegreesAsync(Guid universityId, bool trackChanges)
    {
        return await FindByCondition(d => d.UniversityId.Equals(universityId), trackChanges)
            .OrderBy(d => d.Name)
            .ToListAsync();
    }

    public async Task<Degree> GetDegreeAsync(Guid degreeId, bool trackChanges)
    {
        return await FindByCondition(d => d.Id.Equals(degreeId), trackChanges)
            .SingleOrDefaultAsync();
    }
}

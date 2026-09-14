using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class FacultyDeanRepository : RepositoryBase<FacultyDean>, IFacultyDeanRepository
{
    public FacultyDeanRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateFacultyDean(FacultyDean facultyDean)
    {
        Create(facultyDean);
    }

    public async Task<IEnumerable<FacultyDean>> GetAllFacultyDeansForFacultyAsync(FacultyDeanParameters facultyDeanParameteres, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .FilterFacultyDeans(facultyDeanParameteres.facultyId,facultyDeanParameteres.deanId)
            .ToListAsync();
    }

    public async Task<FacultyDean> GetFacultyDeanAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(fd => fd.Id.Equals(id), trackChanges)
            .Include(fd => fd.Faculty)
            .Include(fd => fd.Dean)
            .SingleOrDefaultAsync();
    }

    public void RemoveFacultyDean(FacultyDean facultyDean)
    {
        Delete(facultyDean);
    }
   public async Task<FacultyDean> GetCurrentFacultyDeanAsync(Guid facultyId, bool trackChanges)
    {
        return await FindByCondition(fd => fd.FacultyId.Equals(facultyId) && fd.EndDate == null, trackChanges)
.SingleOrDefaultAsync();
    }
}

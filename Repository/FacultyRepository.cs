using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Shared.DataTransferObjects;

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

    public IQueryable<Faculty> GetFacultiesAsync(Guid universityId, bool trackChanges)
    {
        return  FindByCondition(
        f => f.UniversityId.Equals(universityId),
        trackChanges);
    }

    public async Task<Faculty> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges)
    {
        return await FindByCondition(f => f.UniversityId.Equals(universityId) && f.Id.Equals(id),
            trackChanges)
            .Include(f => f.FacultyDeans
        .Where(fd => fd.EndDate == null))
        .ThenInclude(fd => fd.Dean)
            .SingleOrDefaultAsync();


    }
    public IQueryable<Faculty> GetFaculty(
    Guid universityId,
    Guid id,
    bool trackChanges)
    {
        return FindByCondition(
            f => f.UniversityId == universityId &&
                 f.Id == id,
            trackChanges);
    }
    public async Task<Faculty> GetFacultyAsync(Guid? id, bool trackChanges)
    {
        return await FindByCondition(f => f.Id.Equals(id),
            trackChanges)
            .Include(f => f.FacultyDeans
        .Where(fd => fd.EndDate == null))
        .ThenInclude(fd => fd.Dean)
            .SingleOrDefaultAsync();
    }

    public async Task<Faculty> GetFacultyByDeanIdAsync(string deanId, bool trackChanges)
    {
        return await FindByCondition(f => f.FacultyDeans.Any(fd => fd.DeanId.Equals(deanId) && fd.EndDate == null), trackChanges)
            .Include(f => f.FacultyDeans
        .Where(fd => fd.EndDate == null))
        .ThenInclude(fd => fd.Dean).SingleOrDefaultAsync(); ;
    }
    public async Task<int> GetCountByUniversityAsync(Guid universityId) { return await FindByCondition(f => f.UniversityId == universityId, false).CountAsync(); }
}

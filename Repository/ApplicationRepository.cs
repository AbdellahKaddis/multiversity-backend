

using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ApplicationRepository : RepositoryBase<Application>, IApplicationRepository
{
    public ApplicationRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateApplication(Application application)
    {
        Create(application);
    }

    public void DeleteApplication(Application application)
    {
        Delete(application);
    }

    public async Task<Application> GetApplicationAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(a => a.Id.Equals(id), trackChanges)
            .Include(a => a.Program)
            .Include(a => a.Applicant)
            .ThenInclude(a => a.Faculty)
              .Include(a => a.Applicant.User)
            .Include(a => a.Reviewer)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Application>> GetApplicationsAsync(ApplicationParameters applicationParameters, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .FilterApplications(applicationParameters.UniversityId, applicationParameters.FacultyId, applicationParameters.ProgramId, applicationParameters.Status, applicationParameters.ApplicantId)
            .Include(a => a.Program)
            .Include(a => a.Applicant)
            .ThenInclude(a => a.Faculty)
            .Include(a => a.Applicant.User)
            .Include(a => a.Reviewer)
            .ToListAsync(); 
    }
}

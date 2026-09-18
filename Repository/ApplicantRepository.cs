

using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class ApplicantRepository : RepositoryBase<Applicant>, IApplicantRepository
{
    public ApplicantRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateApplicant(string userId, Applicant applicant)
    {
        applicant.Id = userId;
        Create(applicant);
    }

    public void DeleteApplicant(Applicant applicant)
    {
        Delete(applicant);
    }

    public async Task<Applicant> GetApplicantAsync(string id, bool trackChanges)
    {
        return await FindByCondition(a => a.Id.Equals(id), trackChanges)
             .Include(a => a.User)
                .Include(a => a.Enrollments.Where(e => e.Status == "Active"))
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Applicant>> GetApplicantsAsync(Guid universityId, ApplicantParameters applicantParameters, bool trackChanges)
    {
        return await FindByCondition(a => a.Faculty.UniversityId.Equals(universityId), trackChanges)
            .FilterApplicants(applicantParameters.FacultyId, applicantParameters.Status)
             .Include(a => a.User)
        .Include(a => a.Enrollments.Where(e => e.Status == "Active"))
            .ToListAsync();
    }

    public async Task<int> GetCountByUniversityAsync(Guid universityId)
    {
         return await FindByCondition(a => a.Faculty.UniversityId == universityId && (bool)a.User.IsActive && a.Status == "Student", false).CountAsync(); 
    }
}

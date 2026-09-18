

using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IApplicantRepository
{
    Task<IEnumerable<Applicant>> GetApplicantsAsync(Guid universityId, ApplicantParameters applicantParameters, bool trackChanges);
    Task<Applicant> GetApplicantAsync(string id, bool trackChanges);
    void CreateApplicant(string userId, Applicant applicant);
    void DeleteApplicant(Applicant applicant);
    Task<int> GetCountByUniversityAsync(Guid universityId);
}

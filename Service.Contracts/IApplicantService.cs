

using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IApplicantService
{
    Task<IEnumerable<ApplicantDto>> GetApplicantsAsync(Guid universityId, ApplicantParameters applicantParameters, bool trackChanges);
    Task<ApplicantDto> GetApplicantAsync(string applicantId, bool trackChanges);
    Task<ApplicantDto> CreateApplicantAsync(ApplicantForRegistrationDto applicantForRegistrationDto);
}

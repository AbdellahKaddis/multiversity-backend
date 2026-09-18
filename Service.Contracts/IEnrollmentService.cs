
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentDto>> GetEnrollmentsAsync(EnrollmentParameters enrollmentParameters, bool trackChanges);
    Task<EnrollmentDto> GetEnrollmentAsync(Guid enrollmentId, bool trackChanges);
    Task<EnrollmentDto> CreateEnrollment(EnrollmentForCreationDto enrollmentForCreationDto);
    Task DeleteEnrollment(Guid enrollmentId, bool trackChanges);
    Task UpdateEnrollment(Guid enrollmentId, EnrollmentForUpdateDto enrollmentForUpdateDto, bool trackChanges);
}

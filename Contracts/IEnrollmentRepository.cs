

using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IEnrollmentRepository
{
    Task<IEnumerable<Enrollment>> GetEnrollmentsAsync(EnrollmentParameters enrollmentParameters, bool trackChanges);
    Task<Enrollment> GetEnrollmentAsync(Guid enrollmentId, bool trackChanges);
    void CreateEnrollment(Enrollment enrollment);
    void DeleteEnrollment(Enrollment enrollment);
    Task<string> GenerateStudentNumberAsync();
    Task<IEnumerable<Enrollment>> GetEnrollmentsForCourseAsync(
    Guid courseId, bool trackChanges);
}

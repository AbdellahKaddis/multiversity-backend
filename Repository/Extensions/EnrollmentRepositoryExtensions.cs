
using Entities.Models;

namespace Repository.Extensions;

public static class EnrollmentRepositoryExtensions
{
    public static IQueryable<Enrollment> FilterEnrollments(
    this IQueryable<Enrollment> enrollments,
    string? applicantId,
    Guid? programId,
    Guid? universityId,
    Guid? facultyId,
    string? academicYear,
    string? studentNumber,
    string? status,
    int? yearLevel)
    {
        if (!string.IsNullOrWhiteSpace(applicantId))
            enrollments = enrollments.Where(e => e.ApplicantId == applicantId);

        if (programId.HasValue)
            enrollments = enrollments.Where(e => e.ProgramId == programId.Value);

        if (universityId.HasValue)
            enrollments = enrollments.Where(e => e.Faculty.UniversityId == universityId.Value);

        if (facultyId.HasValue)
            enrollments = enrollments.Where(e => e.FacultyId == facultyId.Value);

        if (!string.IsNullOrWhiteSpace(academicYear))
            enrollments = enrollments.Where(e => e.AcademicYear == academicYear);

        if (!string.IsNullOrWhiteSpace(studentNumber))
            enrollments = enrollments.Where(e => e.StudentNumber == studentNumber);

        if (!string.IsNullOrWhiteSpace(status))
            enrollments = enrollments.Where(e => e.Status == status);

        if (yearLevel.HasValue)
            enrollments = enrollments.Where(e => e.YearLevel == yearLevel.Value);

        return enrollments;
    }
}

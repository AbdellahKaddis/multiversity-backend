

using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryApplicationExtentsions
{
    public static IQueryable<Application> FilterApplications(
      this IQueryable<Application> applications,
          Guid? UniversityId,
      Guid? facultyId,
      Guid? programId,
      string? status,
      string? applicantId)
    {
        if (UniversityId.HasValue)
            applications = applications.Where(a => a.Applicant.Faculty.UniversityId == UniversityId.Value);

        if (facultyId.HasValue)
            applications = applications.Where(a => a.Applicant.FacultyId == facultyId.Value);

        if (programId.HasValue)
            applications = applications.Where(a => a.ProgramId == programId.Value);

        if (!string.IsNullOrWhiteSpace(status))
            applications = applications.Where(a => a.Status == status);

        if (!string.IsNullOrWhiteSpace(applicantId))
            applications = applications.Where(a => a.ApplicantId == applicantId);

        return applications;
    }
}



using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryApplicantExtensions
{
    public static IQueryable<Applicant> FilterApplicants(this IQueryable<Applicant> applicants, Guid? facultyId, string? status)
    {
        if (facultyId is null && status is null)
            return applicants;
        else if (facultyId is not null && status is null)
            return applicants.Where(a => a.FacultyId.Equals(facultyId));
        else if (facultyId is null && status is not null)
            return applicants.Where(a => a.Status.Equals(status));
        else
            return applicants.Where(a => a.FacultyId.Equals(facultyId) && a.Status.Equals(status));
    }
}


using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions;

public static class RepositoryFacultyDeanExtensions
{
    public static IQueryable<FacultyDean> FilterFacultyDeans(this IQueryable<FacultyDean> facultyDeans, Guid? facultyId, string? deanId)
    {
        if (facultyId is null && deanId is null)
            return facultyDeans;
        else if (facultyId is not null && deanId is null)
            return facultyDeans.Where(fd => fd.FacultyId.Equals(facultyId)).Include(fd => fd.Dean);
        else if (facultyId is null && deanId is not null)
            return facultyDeans.Where(fd => fd.DeanId.Equals(deanId)).Include(fd => fd.Faculty);
        else
            return facultyDeans.Where(fd => fd.FacultyId.Equals(facultyId) && fd.DeanId.Equals(deanId)).Include(fd => fd.Dean).Include(fd => fd.Faculty);
    }
}

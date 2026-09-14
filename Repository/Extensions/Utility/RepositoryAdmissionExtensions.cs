using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Extensions.Utility;

public static class RepositoryAdmissionExtensions
{
    public static IQueryable<Admission> FilterAdmissions(this IQueryable<Admission> admissions, Guid? facultyId, Guid? programId)
    {
        if (facultyId is null && programId is null)
            return admissions;
        else if (facultyId is not null && programId is null)
            return admissions.Where(a => a.Program.Department.FacultyId.Equals(facultyId));
        else if (facultyId is null && programId is not null)
            return admissions.Where(a => a.ProgramId.Equals(programId));
        else
            return admissions.Where(a => a.Program.Department.FacultyId.Equals(facultyId) && a.ProgramId.Equals(programId));
    }
}

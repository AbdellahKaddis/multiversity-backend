using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryAcademicProgramExtensions
{
    public static IQueryable<AcademicProgram> FilterPrograms
        (this IQueryable<AcademicProgram> programs, Guid? facultyId, Guid? departmentId)
    {
        if (facultyId is null && departmentId is null)
            return programs;
        if(facultyId is not null && departmentId is null)
            return programs.Where(p => p.Department.FacultyId == facultyId);

        return programs.Where(p => p.DepartmentId == departmentId);
    }
}

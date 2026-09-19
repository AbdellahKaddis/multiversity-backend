using Entities.Models;

namespace Repository.Extensions;

public static class RepositoryAcademicProgramExtensions
{
    public static IQueryable<AcademicProgram> FilterPrograms
        (this IQueryable<AcademicProgram> programs, Guid? facultyId, Guid? departmentId, Guid? universityId)
    {
        if (universityId.HasValue)
            programs = programs.Where(p => p.Department.Faculty.UniversityId == universityId.Value);

        if (facultyId.HasValue)
            programs = programs.Where(p => p.Department.FacultyId == facultyId.Value);
        if (departmentId.HasValue)
            programs = programs.Where(p => p.DepartmentId == departmentId.Value);

       

        return programs;
    }
}

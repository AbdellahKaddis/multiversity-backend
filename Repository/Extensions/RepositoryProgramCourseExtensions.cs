using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository.Extensions;

public static class RepositoryProgramCourseExtensions
{
    public static IQueryable<ProgramCourse> FilterProgramCourses(this  IQueryable<ProgramCourse> programCourses, Guid? programId)
    {
        if(programId is null)
            return programCourses;
        return programCourses.Where(pc => pc.ProgramId.Equals(programId));
    }
}

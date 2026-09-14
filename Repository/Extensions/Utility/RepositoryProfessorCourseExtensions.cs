
using Entities.Models;
using System;

namespace Repository.Extensions.Utility;

public static class RepositoryProfessorCourseExtensions
{
    public static IQueryable<ProfessorCourse> FilterProfessorCourses(this IQueryable<ProfessorCourse> professorCourses, string? professorId, Guid? courseId)
    {
        if (professorId is null && courseId is null)
            return professorCourses;
        if (professorId is not null && courseId is null)
            return professorCourses.Where(pc => pc.ProfessorId == professorId);

        return professorCourses.Where(pc => pc.CourseId == courseId);
    }
}

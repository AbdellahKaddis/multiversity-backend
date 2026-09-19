using Entities.Models;

namespace Repository.Extensions;

public static class GradeRepositoryExtensions
{
    public static IQueryable<Grade> FilterGrades(
        this IQueryable<Grade> grades,
        Guid? enrollmentId,
        Guid? courseId,
        string? academicYear,
        string? semester,
        string? session,
        bool? isPublished,
        bool? validated)
    {
        if (enrollmentId.HasValue)
            grades = grades.Where(g => g.EnrollmentId == enrollmentId.Value);

        if (courseId.HasValue)
            grades = grades.Where(g => g.CourseId == courseId.Value);

        if (!string.IsNullOrWhiteSpace(academicYear))
            grades = grades.Where(g => g.AcademicYear == academicYear);

        if (!string.IsNullOrWhiteSpace(semester))
            grades = grades.Where(g => g.Semester == semester);

        if (!string.IsNullOrWhiteSpace(session))
            grades = grades.Where(g => g.Session == session);

        if (isPublished.HasValue)
            grades = grades.Where(g => g.IsPublished == isPublished.Value);

        if (validated.HasValue)
            grades = grades.Where(g => g.Validated == validated.Value);

        return grades;
    }
}
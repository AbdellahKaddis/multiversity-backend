using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;

public class GradeRepository : RepositoryBase<Grade>, IGradeRepository
{

    public GradeRepository(RepositoryContext repositoryContext)
        : base(repositoryContext)
    {
       
    }

    public void CreateGrade(Grade grade)
    {
        Create(grade);
    }

    public void DeleteGrade(Grade grade)
    {
        Delete(grade);
    }

    public async Task<Grade> GetGradeAsync(Guid gradeId, bool trackChanges)
    {
        return await FindByCondition(g => g.Id.Equals(gradeId), trackChanges)
            .Include(g => g.Enrollment)
                .ThenInclude(e => e.Applicant.User)
            .Include(g => g.Enrollment.Program)
            .Include(g => g.Course)
            .Include(g => g.Professor)
            .SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<Grade>> GetGradesAsync(
        GradeParameters p, bool trackChanges)
    {
        return await FindAll(trackChanges)
            .Include(g => g.Enrollment)
                .ThenInclude(e => e.Applicant.User)
            .Include(g => g.Enrollment.Program)
            .Include(g => g.Course)
            .Include(g => g.Professor)
            .FilterGrades(
                p.EnrollmentId, p.CourseId, p.AcademicYear,
                p.Semester, p.Session, p.IsPublished, p.Validated)
            .ToListAsync();
    }
    public async Task<IEnumerable<Grade>> GetGradesForPublishAsync(
    Guid courseId, string academicYear, string semester, string session,
    bool trackChanges)
    {
        return await FindByCondition(g =>
                g.CourseId == courseId &&
                g.AcademicYear == academicYear &&
                g.Semester == semester &&
                g.Session == session &&
                !(bool)g.IsPublished,
                trackChanges)
            .ToListAsync();
    }
}
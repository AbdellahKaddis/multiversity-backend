
using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions.Utility;
using Shared.RequestFeatures;

namespace Repository;

public class ProfessorCourseRepository : RepositoryBase<ProfessorCourse>, IProfessorCourseRepository
{
    public ProfessorCourseRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateProfessorCourse(ProfessorCourse professorCourse)
    {
        Create(professorCourse);
    }

    public async Task<IEnumerable<ProfessorCourse>> GetAllProfessorCoursesForFacultyAsync(ProfessorCourseParameters p, bool trackChanges)
    {
        return await FindByCondition(
            pc => pc.Professor.FacultyId.Equals(p.FacultyId),
            trackChanges)
        .FilterProfessorCourses(p.ProfessorId, p.CourseId)
        .Include(pc => pc.Professor)
            .ThenInclude(pr => pr.User)
        .Include(pc => pc.Course)
            .ThenInclude(c => c.ProgramCourses)
                .ThenInclude(pgc => pgc.AcademicProgram)
                    .ThenInclude(ap => ap.Enrollments)
        .ToListAsync();
    }

    public async Task<ProfessorCourse> GetProfessorCourseAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(pc => pc.Id.Equals(id), trackChanges)
                .Include(pc => pc.Professor)
            .ThenInclude(pr => pr.User)
        .Include(pc => pc.Course)
            .ThenInclude(c => c.ProgramCourses)
                .ThenInclude(pgc => pgc.AcademicProgram)
                    .ThenInclude(ap => ap.Enrollments)
            .SingleOrDefaultAsync();
    }

    public void RemoveProfessorCourse(ProfessorCourse professorCourse)
    {
        Delete(professorCourse);
    }
}

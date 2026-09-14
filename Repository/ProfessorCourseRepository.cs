
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

    public async Task<IEnumerable<ProfessorCourse>> GetAllProfessorCoursesForFacultyAsync(ProfessorCourseParameters professorCourseParameters, bool trackChanges)
    {
        return await FindByCondition(pc => pc.Professor.FacultyId.Equals(professorCourseParameters.FacultyId), trackChanges)
            .FilterProfessorCourses(professorCourseParameters.ProfessorId, professorCourseParameters.CourseId)
            .Include(pc => pc.Course)
            .Include(pc => pc.Professor)
            .ThenInclude(p => p.User)
            .ToListAsync();
    }

    public async Task<ProfessorCourse> GetProfessorCourseAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(pc => pc.Id.Equals(id), trackChanges)
               .Include(pc => pc.Course)
            .Include(pc => pc.Professor)
            .ThenInclude(p => p.User)
            .SingleOrDefaultAsync();
    }

    public void RemoveProfessorCourse(ProfessorCourse professorCourse)
    {
        Delete(professorCourse);
    }
}

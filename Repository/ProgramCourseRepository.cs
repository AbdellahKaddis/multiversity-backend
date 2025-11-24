using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions;
using Shared.RequestFeatures;

namespace Repository;
public class ProgramCourseRepository : RepositoryBase<ProgramCourse>, IProgramCourseRepository
{
    public ProgramCourseRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateProgramCourse(ProgramCourse programCourse)
    {
        Create(programCourse);
    }

    public async Task<IEnumerable<ProgramCourse>> GetAllProgramCoursesForFacultyAsync(ProgramCourseParameteres programCourseParameteres, bool trackChanges)
    {
        return await FindByCondition(pc => pc.AcademicProgram.Department.FacultyId.Equals(programCourseParameteres.facultyId), trackChanges)
            .Include(pc => pc.Course)
            .FilterProgramCourses(programCourseParameteres.programId)
            .ToListAsync();
    }

    public async Task<ProgramCourse> GetProgramCourseAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(pc => pc.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }

    public void RemoveProgramCourse(ProgramCourse programCourse)
    {
        Delete(programCourse);
    }
    public async Task<IEnumerable<ProgramCourse>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        trackChanges) =>
         await FindByCondition(x => ids.Contains(x.Id), trackChanges)
        .Include(pc => pc.Course)
         .ToListAsync();
}

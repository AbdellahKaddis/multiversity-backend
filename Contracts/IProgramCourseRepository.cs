using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IProgramCourseRepository
{
    Task<IEnumerable<ProgramCourse>> GetAllProgramCoursesForFacultyAsync(ProgramCourseParameteres programCourseParameteres, bool trackChanges);
    Task<ProgramCourse> GetProgramCourseAsync(Guid id, bool trackChanges);
    void CreateProgramCourse(ProgramCourse programCourse);
    void RemoveProgramCourse(ProgramCourse programCourse);
    Task<IEnumerable<ProgramCourse>> GetByIdsAsync(IEnumerable<Guid> ids, bool
        trackChanges);
}

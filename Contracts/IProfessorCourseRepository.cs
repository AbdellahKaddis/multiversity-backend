
using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IProfessorCourseRepository
{
    Task<IEnumerable<ProfessorCourse>> GetAllProfessorCoursesForFacultyAsync(ProfessorCourseParameters professorCourseParameters, bool trackChanges);
    Task<ProfessorCourse> GetProfessorCourseAsync(Guid id, bool trackChanges);
    void CreateProfessorCourse(ProfessorCourse professorCourse);
    void RemoveProfessorCourse(ProfessorCourse professorCourse);
}

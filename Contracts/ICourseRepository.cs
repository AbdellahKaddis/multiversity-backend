using Entities.Models;

namespace Contracts;

public interface ICourseRepository
{
    Task<IEnumerable<Course>> GetAllCoursesAsync(Guid facultyId, bool trackChanges);
    Task<Course> GetCourseAsync(Guid id, bool trackChanges);
    void CreateCourse(Guid facultyId, Course course);
    void DeleteCourse(Course course);
}

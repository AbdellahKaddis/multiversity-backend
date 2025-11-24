using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class CourseRepository : RepositoryBase<Course>, ICourseRepository
{
    public CourseRepository(RepositoryContext repositoryContext) 
        : base(repositoryContext) { }
    public void CreateCourse(Guid facultyId, Course course)
    {
        course.FacultyId = facultyId;
        Create(course);
    }

    public void DeleteCourse(Course course)
    {
        Delete(course);
    }

    public async Task<IEnumerable<Course>> GetAllCoursesAsync(Guid facultyId, bool trackChanges)
    {
        return await FindByCondition(c => c.FacultyId.Equals(facultyId), trackChanges)
            .ToListAsync();
    }

    public async Task<Course> GetCourseAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(c => c.Id.Equals(id), trackChanges)
            .SingleOrDefaultAsync();
    }
}

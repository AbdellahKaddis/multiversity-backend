using Entities.Models;

namespace Entities.Exceptions;

public class CourseNotFoundException : NotFoundException
{
    public CourseNotFoundException(Guid courseId)
        :base($"Course with id: {courseId} doesn't exist in the database.") { }
}

using Entities.Models;

namespace Entities.Exceptions;
public class ProgramCourseNotFoundException : NotFoundException
{
    public ProgramCourseNotFoundException(Guid id):
        base($"ProgramCourse with id: {id} doesn't exist in the database.")
    { }
}

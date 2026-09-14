

namespace Entities.Exceptions;

public sealed class ProfessorCourseNotFoundException : NotFoundException
{
    public ProfessorCourseNotFoundException(Guid id) : base($"Professor Course with id: {id} doesn't exist in the database.") { }
}

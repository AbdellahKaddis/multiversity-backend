using Entities.Models;

namespace Entities.Exceptions;
public class FacultyDeanNotFoundException : NotFoundException
{
    public FacultyDeanNotFoundException(Guid id)
        : base($"FacultyFean with id: {id} doesn't exist in the database.") { }
}

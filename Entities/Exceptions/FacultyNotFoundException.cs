using Entities.Models;

namespace Entities.Exceptions;

public class FacultyNotFoundException : NotFoundException
{
    public FacultyNotFoundException(Guid facultyId)
        :base($"Faculty with id: {facultyId} doesn't exist in the database.")
    {

    }
}

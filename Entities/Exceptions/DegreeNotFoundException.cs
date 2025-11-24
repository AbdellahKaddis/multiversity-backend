using Entities.Models;

namespace Entities.Exceptions;

public class DegreeNotFoundException : NotFoundException
{
    public DegreeNotFoundException(Guid degreeId)
        : base($"Degree with id: {degreeId} doesn't exist in the database.")
    {

    }
}

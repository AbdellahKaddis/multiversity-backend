using Entities.Models;

namespace Entities.Exceptions;
public class AcademicProgramNotFoundException : NotFoundException
{
    public AcademicProgramNotFoundException(Guid programId)
        :base($"Academic program with id: {programId} doesn't exist in the database.") { }
}

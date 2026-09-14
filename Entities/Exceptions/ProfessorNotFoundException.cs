using Entities.Models;

namespace Entities.Exceptions;

public class ProfessorNotFoundException : NotFoundException
{
    public ProfessorNotFoundException(string professorId) :
        base($"Professor with id: {professorId} doesn't exist in the database.")
    {

    }
}

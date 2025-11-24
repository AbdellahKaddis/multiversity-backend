using Entities.Models;

namespace Entities.Exceptions;

public class DeanForFacultyNotFoundException : NotFoundException
{
    public DeanForFacultyNotFoundException(string deanId)
        : base($"No faculty with dean id: {deanId}.")
    {

    }
}

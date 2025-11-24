using Entities.Models;

namespace Entities.Exceptions;

public class DepartmentNotFoundException : NotFoundException
{
    public DepartmentNotFoundException(Guid? departmentId)
        : base($"Department with id: {departmentId} doesn't exist in the database.")
    {

    }
}

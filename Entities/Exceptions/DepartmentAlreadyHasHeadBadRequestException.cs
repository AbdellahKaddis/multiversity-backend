
namespace Entities.Exceptions;

public sealed class DepartmentAlreadyHasHeadBadRequestException : BadRequestException
{
    public DepartmentAlreadyHasHeadBadRequestException(Guid id):
        base($"department with Id : {id} already has a head.")
    {

    }
}


namespace Entities.Exceptions;

public sealed class UserRoleAssignmentBadRequestException: BadRequestException
{
    public UserRoleAssignmentBadRequestException(string message): base(message) { }
}

namespace Entities.Exceptions;

public sealed class UserCreationBadRequestException : BadRequestException
{
    public UserCreationBadRequestException(string message):base(message) { }
}

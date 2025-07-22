namespace Entities.Exceptions;
public sealed class InvalidCodeBadRequestException : BadRequestException
{
    public InvalidCodeBadRequestException()
    : base("Invalid code")
    {
    }
}


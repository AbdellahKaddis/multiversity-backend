namespace Entities.Exceptions;

public class UniversityDuplicateValuesBadRequestException : BadRequestException
{
    public UniversityDuplicateValuesBadRequestException(string message):base(message) { }
}

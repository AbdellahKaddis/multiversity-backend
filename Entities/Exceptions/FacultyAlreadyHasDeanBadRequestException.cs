
namespace Entities.Exceptions;

public sealed class FacultyAlreadyHasDeanException : BadRequestException
{
    public FacultyAlreadyHasDeanException(Guid facultyId)
    : base($"Faculty with Id : {facultyId} has already a dean.")
    {
    }
}

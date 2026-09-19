
using Entities.Exceptions;

public sealed class NoGradesToPublishException : BadRequestException
{
    public NoGradesToPublishException(
        Guid courseId, string academicYear, string semester, string session)
        : base($"No unpublished grades found for course {courseId} " +
               $"({academicYear}, {semester}, {session}).")
    { }
}
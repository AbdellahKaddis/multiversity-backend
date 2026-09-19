using Entities.Exceptions;

public sealed class GradeNotFoundException : NotFoundException
{
    public GradeNotFoundException(Guid id)
        : base($"The grade with id: {id} doesn't exist in the database.") { }
}

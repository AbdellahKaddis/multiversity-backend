using System;
namespace Entities.Exceptions;

public class ApplicationNotFoundException : NotFoundException
{
    public ApplicationNotFoundException(Guid applicationId) : base($"Application with Id : {applicationId} does not exist in the database.") { }
}

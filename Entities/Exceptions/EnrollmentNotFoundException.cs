
using System;
namespace Entities.Exceptions;

public class EnrollmentNotFoundException : NotFoundException
{
    public EnrollmentNotFoundException(Guid enrollmentId) : base($"Enrollment with Id : {enrollmentId} does not exist in the database.") { }
}


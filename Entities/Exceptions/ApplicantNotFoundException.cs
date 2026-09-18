using Entities.Models;

namespace Entities.Exceptions;

public class ApplicantNotFoundException : NotFoundException
{
    public ApplicantNotFoundException(string applicantId) :
        base($"Applicant with id: {applicantId} doesn't exist in the database.")
    {

    }
}

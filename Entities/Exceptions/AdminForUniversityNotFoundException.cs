namespace Entities.Exceptions;

public class AdminForUniversityNotFoundException : NotFoundException
{
    public AdminForUniversityNotFoundException(string adminId)
        :base($"No university with admin id: {adminId}.")
    {

    }
}

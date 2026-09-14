
namespace Entities.Exceptions;

public sealed class UserNotFoundException : NotFoundException
{
    public UserNotFoundException(string userId)
    : base($"User with Id {userId} doesn't exist in the database.")
    {
    }
}



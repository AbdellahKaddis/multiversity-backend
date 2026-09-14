
namespace Entities.Exceptions;

public sealed class UserDeactivationException : Exception
{
    public UserDeactivationException()
    : base("Failed to deactivate user")
    { }
}


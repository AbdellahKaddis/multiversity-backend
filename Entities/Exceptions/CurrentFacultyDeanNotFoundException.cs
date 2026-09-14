
namespace Entities.Exceptions;

public sealed class CurrentFacultyDeanNotFoundException : NotFoundException
{
    public CurrentFacultyDeanNotFoundException()
    : base("current faculty dean was not found.")
    {
    }
}



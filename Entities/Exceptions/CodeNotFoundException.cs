
namespace Entities.Exceptions;

public sealed class CodeNotFoundException : NotFoundException
{
    public CodeNotFoundException()
    : base("Code expired or not found.")
    {
    }
}


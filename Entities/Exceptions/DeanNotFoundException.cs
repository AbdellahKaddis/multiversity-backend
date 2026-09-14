using Entities.Models;

namespace Entities.Exceptions;
public class DeanNotFoundException : NotFoundException
{
    public DeanNotFoundException(string deanId)
        : base($"Dean with id: {deanId} doesn't exist in the database.") { }
}

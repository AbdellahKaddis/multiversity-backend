
namespace Entities.Exceptions
{
    public sealed class UniversityNotFoundException : NotFoundException
    {
        public UniversityNotFoundException(Guid universityId)
        : base($"The University with id: {universityId} doesn't exist in the database.")
        {
        }
    }
}

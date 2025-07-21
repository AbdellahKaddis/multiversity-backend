using Entities.Models;

namespace Contracts;

public interface IUniversityRepository
{
    Task<IEnumerable<University>> GetAllUniversitiesAsync(bool trackChanges);
    Task<University> GetUniversityAsync(Guid univeristyId, bool trackChanges);
    void CreateUniversity(University university);
    void DeleteUniversity(University university);
}

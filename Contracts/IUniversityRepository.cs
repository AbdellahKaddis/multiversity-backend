using Entities.Models;
using Shared.DataTransferObjects;

namespace Contracts;

public interface IUniversityRepository
{
    Task<IEnumerable<University>> GetAllUniversitiesAsync(bool trackChanges);
    Task<University> GetUniversityAsync(Guid univeristyId, bool trackChanges);
    void CreateUniversity(University university);
    void DeleteUniversity(University university);
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NameExistsAsync(string name);
    Task<bool> PhoneNumberExistsAsync(string phoneNumber);
    Task<List<string>> CheckForDuplicatesAsync(
       string email,
       string name,
       string phoneNumber);
}

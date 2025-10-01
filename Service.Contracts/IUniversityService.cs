using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IUniversityService
{
    Task<IEnumerable<UniversityDto>> GetAllUniversitiesAsync(bool trackChanges);
    Task<UniversityDto> GetUniversityAsync(Guid universityId, bool trackChanges);
    Task<UniversityDto> CreateUniversityAsync(UniversityForCreationDto university);
    Task DeleteUniversityAsync(Guid universityId, bool trackChanges);
    Task UpdateUniversityAsync(Guid universityId, UniversityForUpdateDto universityForUpdate,
    bool trackChanges);

    Task CheckForDuplicatesAsync(
       string email,
       string name,
       string? phoneNumber);
    Task<UniversityDto> GetUniversityByAdminIdAsync(string adminId, bool trackChanges);
}

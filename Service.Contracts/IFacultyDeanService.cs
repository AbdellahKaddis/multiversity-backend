using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IFacultyDeanService
{
    Task<IEnumerable<FacultyDeanDto>> GetAllFacultyDeansAsync(FacultyDeanParameters facultyDeanParameteres, bool trackChanges);
    Task<FacultyDeanDto> GetFacultyDeanAsync(Guid id, bool trackChanges);
    Task<FacultyDeanDto> CreateFacultyDeanAsync(FacultyDeanForCreationDto facultyDeanForCreationDto);
    Task RemoveFacultyDeanAsync(Guid id, bool trackChanges);
    Task UpdateFacultyDeanAsync(Guid id, FacultyDeanForUpdateDto facultyDeanForUpdateDto,
    bool trackChanges);
    Task<FacultyDeanDto> GetCurrentFacultyDeanAsync(Guid facultyId, bool trackChanges);

    Task CreateAndAssignDeanAsync(Guid facultyId, FacultyDeanForRegistrationDto deanForRegistrationDto);
}

using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;
public interface IFacultyService
{
    Task<IEnumerable<FacultyDto>> GetFacultiesAsync
        (Guid universityId, bool trackChanges);
    Task<FacultyDto> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges);
    Task<FacultyDto> CreateFacultyForUniversityAsync(Guid universityId, FacultyForCreationDto
    facultyForCreation, bool trackChanges);
    Task DeleteFacultyForUniversityAsync(Guid universityId, Guid id, bool trackChanges);
    Task UpdateFacultyForUniversityAsync(Guid universityId, Guid id,
    FacultyForUpdateDto facultyForUpdate, bool uniTrackChanges, bool
    facTrackChanges);

}

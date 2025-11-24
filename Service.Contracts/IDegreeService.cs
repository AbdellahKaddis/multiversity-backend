using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface IDegreeService
{
    Task<IEnumerable<DegreeDto>> GetAllDegreesAsync(Guid universityId, bool trackChanges);
    Task<DegreeDto> GetDegreeAsync(Guid degreeId, bool trackChanges);
    Task<DegreeDto> CreateDegree(Guid universityId, DegreeForCreationDto degree);
    Task DeleteDegree(Guid degreeId, bool trackChanges);
    Task UpdateDegree(Guid degreeId, DegreeForUpdateDto degree, bool trackChanges);
}

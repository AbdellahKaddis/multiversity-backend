using Entities.Models;

namespace Contracts;
public interface IDegreeRepository
{
    Task<IEnumerable<Degree>> GetAllDegreesAsync(Guid universityId, bool trackChanges);
    Task<Degree> GetDegreeAsync(Guid degreeId, bool trackChanges);
    void CreateDegree(Guid universityId, Degree degree);
    void DeleteDegree(Degree degree);
}

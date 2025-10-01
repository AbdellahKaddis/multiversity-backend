using Entities.Models;

namespace Contracts;
public interface IFacultyRepository
{
    Task<IEnumerable<Faculty>> GetFacultiesAsync(Guid universityId, bool trackChanges);
    Task<Faculty> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges);
    void CreateFacultyForUniversity(Guid universityId, Faculty faculty);
    void DeleteFaculty(Faculty faculty);
}

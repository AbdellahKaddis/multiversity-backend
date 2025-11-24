using Entities.Models;

namespace Contracts;
public interface IFacultyRepository
{
    Task<IEnumerable<Faculty>> GetFacultiesAsync(Guid universityId, bool trackChanges);
    Task<Faculty> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges);
    Task<Faculty> GetFacultyAsync(Guid? facultyId, bool trackChanges);
    void CreateFacultyForUniversity(Guid universityId, Faculty faculty);
    void DeleteFaculty(Faculty faculty);
    Task<Faculty> GetFacultyByDeanIdAsync(string deanId, bool trackChanges);
}

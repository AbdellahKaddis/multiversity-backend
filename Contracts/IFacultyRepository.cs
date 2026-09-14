using Entities.Models;

namespace Contracts;
public interface IFacultyRepository
{
    IQueryable<Faculty> GetFacultiesAsync(Guid universityId, bool trackChanges);
    Task<Faculty> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges);
    IQueryable<Faculty> GetFaculty(
    Guid universityId,
    Guid id,
    bool trackChanges);
    Task<Faculty> GetFacultyAsync(Guid? facultyId, bool trackChanges);
    void CreateFacultyForUniversity(Guid universityId, Faculty faculty);
    void DeleteFaculty(Faculty faculty);
    Task<Faculty> GetFacultyByDeanIdAsync(string deanId, bool trackChanges);
    Task<int> GetCountByUniversityAsync(Guid universityId);
}

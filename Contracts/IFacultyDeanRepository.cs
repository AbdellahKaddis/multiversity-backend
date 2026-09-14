using Entities.Models;
using Shared.RequestFeatures;

public interface IFacultyDeanRepository
{
    Task<IEnumerable<FacultyDean>> GetAllFacultyDeansForFacultyAsync(FacultyDeanParameters facultyDeanParameteres, bool trackChanges);
    Task<FacultyDean> GetFacultyDeanAsync(Guid id, bool trackChanges);
    void CreateFacultyDean(FacultyDean facultyDean);
    void RemoveFacultyDean(FacultyDean facultyDean);

    Task<FacultyDean> GetCurrentFacultyDeanAsync(Guid facultyId, bool trackChanges);

}


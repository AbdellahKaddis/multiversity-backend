using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IProfessorRepository
{
    Task<IEnumerable<Professor>> GetProfessorsAsync(ProfessorParameteres professorParameteres, bool trackChanges);
    Task<Professor> GetProfessorAsync(string id, bool trackChanges);
    void CreateProfessor(string? userId, Professor professor);
    void DeleteProfessor(Professor professor);
    Task<bool> IsDepartmentHasHeadAsync(Guid facultyId, Guid departmentId, bool trackChanges);
    Task<int> GetCountByUniversityAsync(Guid universityId);
}

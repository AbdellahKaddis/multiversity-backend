using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;
public interface IAcademicProgramRepository
{
    Task<IEnumerable<AcademicProgram>> GetProgramsAsync(ProgramParameters programParameters, bool trackChanges);
    Task<AcademicProgram> GetProgramAsync(Guid departmentId, Guid id, bool trackChanges);
    Task<AcademicProgram> GetProgramAsync(Guid id, bool trackChanges);
    void CreateProgramForDepartment(Guid departmentId, AcademicProgram program);
    void DeleteProgram(AcademicProgram program);
}

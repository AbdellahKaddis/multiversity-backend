using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;
public interface IAcademicProgramService
{
    Task<IEnumerable<AcademicProgramDto>> GetProgramsAsync
    (ProgramParameters programParameters, bool trackChanges);
    Task<AcademicProgramDto> GetProgramAsync(Guid id, bool trackChanges);
    Task<AcademicProgramDto> CreateProgramForDepatmentAsync(Guid departmentId, AcademicProgramForCreationDto
    programForCreationDto, bool trackChanges);
    Task DeleteProgramAsync(Guid id, bool trackChanges);
    Task UpdateProgramAsync(Guid id, AcademicProgramForUpdateDto programForUpdateDto, bool trackChanges);
}

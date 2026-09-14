using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;
public interface IProfessorService
{
    Task<IEnumerable<ProfessorDto>> GetAllProfessorsAsync(ProfessorParameteres professorParameteres, bool trackChanges);
    Task<ProfessorDto> GetProfessorAsync(string professorId, bool trackChanges);
    Task<ProfessorDto> CreateProfessorAsync(ProfessorForCreationDto professorForCreationDto);
    Task DeleteProfessorAsync(string professorId, bool trackChanges);
    Task UpdateProfessorAsync(string professorId, ProfessorForUpdateDto professorForUpdateDto, bool trackChanges);
}

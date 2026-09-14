

using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IProfessorCourseService
{
    Task<IEnumerable<ProfessorCourseDto>> GetAllProfessorCoursesAsync(ProfessorCourseParameters professorCourseParameters, bool trackChanges);
    Task<ProfessorCourseDto> GetProfessorCourseAsync(Guid id, bool trackChanges);
    Task<ProfessorCourseDto> CreateProfessorCourseAsync(ProfessorCourseForCreationDto professorCourseForCreationDto);
    Task RemoveProfessorCourseAsync(Guid id, bool trackChanges);
    Task UpdateProfessorCourseAsync(Guid id, ProfessorCourseForUpdateDto professorCourseForUpdateDto,
    bool trackChanges);
}

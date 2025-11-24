using Entities.Exceptions;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;
public interface IProgramCourseService
{
    Task<IEnumerable<ProgramCourseDto>> GetAllProgramCoursesAsync(ProgramCourseParameteres programCourseParameteres, bool trackChanges);
    Task<ProgramCourseDto> GetProgramCourseAsync(Guid id, bool trackChanges);
    Task<ProgramCourseDto> CreateProgramCourseAsync(ProgramCourseForCreationDto programCourseForCreationDto);
    Task RemoveProgramCourseAsync(Guid id, bool trackChanges);
    Task UpdateProgramCourseAsync(Guid id, ProgramCourseForUpdateDto programCourseForUpdateDto,
    bool trackChanges);
    Task<IEnumerable<ProgramCourseDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges);
    Task<(IEnumerable<ProgramCourseDto> programCourses, string ids)> CreateProgramCourseCollectionAsync
        (IEnumerable<ProgramCourseForCreationDto> programCollection);
}

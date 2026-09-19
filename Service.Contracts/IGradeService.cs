using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service.Contracts;

public interface IGradeService
{
    Task<GradeDto> CreateGrade(GradeForCreationDto gradeForCreationDto);
    Task DeleteGrade(Guid gradeId, bool trackChanges);
    Task<GradeDto> GetGradeAsync(Guid gradeId, bool trackChanges);
    Task<IEnumerable<GradeDto>> GetGradesAsync(GradeParameters gradeParameters, bool trackChanges);
    Task UpdateGrade(Guid gradeId, GradeForUpdateDto gradeForUpdateDto, bool trackChanges);
    Task PublishGradesAsync(PublishGradesDto dto, bool trackChanges);
    Task<SemesterResultDto> GetSemesterResultAsync(
    Guid enrollmentId, string semester, bool trackChanges);
}
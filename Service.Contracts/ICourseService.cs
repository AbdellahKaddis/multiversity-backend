using Shared.DataTransferObjects;

namespace Service.Contracts;
public interface ICourseService
{
    Task<IEnumerable<CourseDto>> GetAllCoursesAsync(Guid facultyId, bool trackChanges);
    Task<CourseDto> GetCourseAsync(Guid courseId, bool trackChanges);
    Task<CourseDto> CreateCourseAsync(Guid facultyId, CourseForCreationDto courseForCreationDto);
    Task DeleteCourseAsync(Guid courseId, bool trackChanges);
    Task UpdateCourseAsync(Guid courseId, CourseForUpdateDto courseForUpdateDto,
    bool trackChanges);
}

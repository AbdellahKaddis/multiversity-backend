using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
public class CourseService : ICourseService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public CourseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<CourseDto> CreateCourseAsync(Guid facultyId, CourseForCreationDto courseForCreationDto)
    {
        await CheckIfFacultyExists(facultyId, false);

        var courseEntity = _mapper.Map<Course>(courseForCreationDto);

        _repository.Course.CreateCourse(facultyId, courseEntity);
        await _repository.SaveAsync();

        var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
        return courseToReturn;
    }

    public async Task DeleteCourseAsync(Guid courseId, bool trackChanges)
    {
        var courseEntity = await GetCourseAndCheckIfItExists(courseId, trackChanges);

        _repository.Course.DeleteCourse(courseEntity);

        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<CourseDto>> GetAllCoursesAsync(Guid facultyId, bool trackChanges)
    {
        var courseEntities = await _repository.Course.GetAllCoursesAsync(facultyId, trackChanges);
        var coursesDto = _mapper.Map<IEnumerable<CourseDto>>(courseEntities);
        return coursesDto;
    }

    public async Task<CourseDto> GetCourseAsync(Guid courseId, bool trackChanges)
    {
        var courseEntity = await GetCourseAndCheckIfItExists(courseId, trackChanges);
        var courseDto = _mapper.Map<CourseDto>(courseEntity);
        return courseDto;
    }

    public async Task UpdateCourseAsync(Guid courseId, CourseForUpdateDto courseForUpdateDto, bool trackChanges)
    {

        var courseEntity = await GetCourseAndCheckIfItExists(courseId, trackChanges);
        
        _mapper.Map(courseForUpdateDto, courseEntity);

        await _repository.SaveAsync();
    }
    private async Task<Course> GetCourseAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var course = await _repository.Course.GetCourseAsync(id, trackChanges);
        if (course is null)
            throw new CourseNotFoundException(id);

        return course;
    }
    private async Task CheckIfFacultyExists(Guid facultyId, bool trackChanges)
    {

        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }
}

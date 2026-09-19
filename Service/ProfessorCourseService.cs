
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class ProfessorCourseService : IProfessorCourseService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public ProfessorCourseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<ProfessorCourseDto> CreateProfessorCourseAsync(ProfessorCourseForCreationDto professorCourseForCreationDto)
    {
        await CheckIfProfessorExists(professorCourseForCreationDto.ProfessorId, false);

        await CheckIfCourseExists(professorCourseForCreationDto.CourseId, false);

        var professorCourseEntity = _mapper.Map<ProfessorCourse>(professorCourseForCreationDto);

        _repository.ProfessorCourse.CreateProfessorCourse(professorCourseEntity);
        await _repository.SaveAsync();

        var professorCourseToReturn = _mapper.Map<ProfessorCourseDto>(professorCourseEntity);
        return professorCourseToReturn;
    }

    public async Task<IEnumerable<ProfessorCourseDto>> GetAllProfessorCoursesAsync(ProfessorCourseParameters p, bool trackChanges)
    {
        await CheckIfFacultyExists(p.FacultyId, trackChanges);

        if(p.ProfessorId is not null)
            await CheckIfProfessorExists(p.ProfessorId, false);

        if (p.CourseId is not null)
            await CheckIfCourseExists((Guid)p.CourseId, false);

        var entities = await _repository.ProfessorCourse
        .GetAllProfessorCoursesForFacultyAsync(p, trackChanges);

        var dtos = _mapper.Map<IEnumerable<ProfessorCourseDto>>(entities);
        return dtos;
    }

    public async Task<ProfessorCourseDto> GetProfessorCourseAsync(Guid id, bool trackChanges)
    {
        var proessorCourseEntity = await GetProfessorCourseAndCheckIfItExists(id, trackChanges);
        var programCourseDto = _mapper.Map<ProfessorCourseDto>(proessorCourseEntity);
        return programCourseDto;
    }

    public async Task RemoveProfessorCourseAsync(Guid id, bool trackChanges)
    {
        var professorCourseEntity = await GetProfessorCourseAndCheckIfItExists(id, trackChanges);

        _repository.ProfessorCourse.RemoveProfessorCourse(professorCourseEntity);
        await _repository.SaveAsync();
    }

    public async Task UpdateProfessorCourseAsync(Guid id, ProfessorCourseForUpdateDto professorCourseForUpdateDto, bool trackChanges)
    {
        var professorCourseEntity = await GetProfessorCourseAndCheckIfItExists(id, trackChanges);

        await CheckIfProfessorExists(professorCourseForUpdateDto.ProfessorId, false);
        await CheckIfCourseExists(professorCourseForUpdateDto.CourseId, false);

        _mapper.Map(professorCourseForUpdateDto, professorCourseEntity);

        await _repository.SaveAsync();
    }

    private async Task CheckIfCourseExists(Guid courseId, bool trackChanges)
    {

        var course = await _repository.Course.GetCourseAsync(courseId, trackChanges);
        if (course is null)
            throw new CourseNotFoundException(courseId);
    }
    private async Task CheckIfFacultyExists(Guid facultyId, bool trackChanges)
    {

        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }

    private async Task CheckIfProfessorExists(string professorId, bool trackChanges)
    {
        var professor = await _repository.Professor.GetProfessorAsync(professorId, trackChanges);
        if (professor is null)
            throw new ProfessorNotFoundException(professorId);
    }
    private async Task<ProfessorCourse> GetProfessorCourseAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var professorCourse = await _repository.ProfessorCourse.GetProfessorCourseAsync(id, trackChanges);
        if (professorCourse is null)
            throw new ProfessorCourseNotFoundException(id);

        return professorCourse;
    }
}

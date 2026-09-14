using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.Extensions.Logging;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;
public class ProgramCourseService : IProgramCourseService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public ProgramCourseService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<ProgramCourseDto> CreateProgramCourseAsync(ProgramCourseForCreationDto programCourseForCreationDto)
    {
        await CheckIfProgramExists(programCourseForCreationDto.ProgramId, false);

        await CheckIfCourseExists(programCourseForCreationDto.CourseId, false);

        var programCourseEntity = _mapper.Map<ProgramCourse>(programCourseForCreationDto);

        _repository.ProgramCourse.CreateProgramCourse(programCourseEntity);
        await _repository.SaveAsync();

        var programCourseToReturn = _mapper.Map<ProgramCourseDto>(programCourseEntity);
        return programCourseToReturn;
    }

    public async Task<IEnumerable<ProgramCourseDto>> GetAllProgramCoursesAsync(ProgramCourseParameteres programCourseParameteres, bool trackChanges)
    {
        await CheckIfFacultyExists(programCourseParameteres.facultyId, trackChanges);
        if (programCourseParameteres.programId is not null)
            await CheckIfProgramExists((Guid)programCourseParameteres.programId, false);
        var programCoursesEntities = await _repository.ProgramCourse.GetAllProgramCoursesForFacultyAsync(programCourseParameteres, trackChanges);
        
        var programCoursesDto = _mapper.Map<IEnumerable<ProgramCourseDto>>(programCoursesEntities);
        return programCoursesDto;
    }

    public async Task<ProgramCourseDto> GetProgramCourseAsync(Guid id, bool trackChanges)
    {
        var programCourseEntity = await GetProgramCourseAndCheckIfItExists(id, trackChanges);
        var programCourseDto = _mapper.Map<ProgramCourseDto>(programCourseEntity);
        return programCourseDto;
    }

    public async Task RemoveProgramCourseAsync(Guid id, bool trackChanges)
    {
        var programCourseEntity = await GetProgramCourseAndCheckIfItExists(id, trackChanges);
        
        _repository.ProgramCourse.RemoveProgramCourse(programCourseEntity);
        await _repository.SaveAsync();
    }

    public async Task UpdateProgramCourseAsync(Guid id, ProgramCourseForUpdateDto programCourseForUpdateDto, bool trackChanges)
    {
        var programCourseEntity = await GetProgramCourseAndCheckIfItExists(id, trackChanges);

        await CheckIfProgramExists(programCourseForUpdateDto.ProgramId, false);

        await CheckIfCourseExists(programCourseForUpdateDto.CourseId, false);

        _mapper.Map(programCourseForUpdateDto, programCourseEntity);

        await _repository.SaveAsync();
    }
    public async Task<IEnumerable<ProgramCourseDto>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges)
    {
        if (ids is null)
            throw new IdParametersBadRequestException();

        var programCourseEntities = await _repository.ProgramCourse.GetByIdsAsync(ids, trackChanges);
        if (ids.Count() != programCourseEntities.Count())
            throw new CollectionByIdsBadRequestException();

        var programCoursesToReturn = _mapper.Map<IEnumerable<ProgramCourseDto>>(programCourseEntities);

        return programCoursesToReturn;
    }

    public async Task<(IEnumerable<ProgramCourseDto> programCourses, string ids)> CreateProgramCourseCollectionAsync
        (IEnumerable<ProgramCourseForCreationDto> programCollection)
    {
        if (programCollection is null)
            throw new ProgramCourseCollectionBadRequest();

        var programCourseEntities = _mapper.Map<IEnumerable<ProgramCourse>>(programCollection);
        foreach (var programCourse in programCourseEntities)
        {
            await CheckIfProgramExists(programCourse.ProgramId, false);

            await CheckIfCourseExists(programCourse.CourseId, false);

            _repository.ProgramCourse.CreateProgramCourse(programCourse);
        }

        await _repository.SaveAsync();

        var programCourseCollectionToReturn = _mapper.Map<IEnumerable<ProgramCourseDto>>(programCourseEntities);
        var ids = string.Join(",", programCourseCollectionToReturn.Select(c => c.Id));

        return (programCourses: programCourseCollectionToReturn, ids: ids);
    }
    private async Task CheckIfProgramExists(Guid programId, bool trackChanges)
    {

        var program = await _repository.Program.GetProgramAsync(programId, trackChanges);
        if (program is null)
            throw new AcademicProgramNotFoundException(programId);
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
    private async Task<ProgramCourse> GetProgramCourseAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var programCourse = await _repository.ProgramCourse.GetProgramCourseAsync(id, trackChanges);
        if (programCourse is null)
            throw new ProgramCourseNotFoundException(id);

        return programCourse;
    }
}

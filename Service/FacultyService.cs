using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service;

public class FacultyService : IFacultyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public FacultyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<FacultyDto> CreateFacultyForUniversityAsync(Guid universityId, FacultyForCreationDto facultyForCreation, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, trackChanges);

        var facultyEntity = _mapper.Map<Faculty>(facultyForCreation);

        _repository.Faculty.CreateFacultyForUniversity(universityId, facultyEntity);
        await _repository.SaveAsync();

        var facultyToReturn = _mapper.Map<FacultyDto>(facultyEntity);

        return facultyToReturn;
    }

    public async Task DeleteFacultyForUniversityAsync(Guid universityId, Guid id, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, trackChanges);

        var facultyEntity = await GetFacultyForUniversityAndCheckIfItExists(universityId, id, trackChanges);

        _repository.Faculty.DeleteFaculty(facultyEntity);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<FacultyDto>> GetFacultiesAsync(Guid universityId, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, trackChanges);

        var faculties = await _repository.Faculty.GetFacultiesAsync(universityId, trackChanges);
        var facultiesDto = _mapper.Map<IEnumerable<FacultyDto>>(faculties);
        return facultiesDto;
    }

    public async Task<FacultyDto> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, trackChanges);

        var facultyEntity = await GetFacultyForUniversityAndCheckIfItExists(universityId, id, trackChanges);

        var facultyDto = _mapper.Map<FacultyDto>(facultyEntity);
        return facultyDto;
    }

    public async Task UpdateFacultyForUniversityAsync(Guid universityId, Guid id, FacultyForUpdateDto facultyForUpdate, bool uniTrackChanges, bool facTrackChanges)
    {
        await CheckIfUniversityExists(universityId, uniTrackChanges);

        var facultyEntity = await GetFacultyForUniversityAndCheckIfItExists(universityId, id, facTrackChanges);

        _mapper.Map(facultyForUpdate, facultyEntity);
        await _repository.SaveAsync();
    }
    public async Task<FacultyDto> GetFacultyByDeanIdAsync(string deanId, bool trackChanges)
    {
        var facultyEntity = await _repository.Faculty.GetFacultyByDeanIdAsync(deanId, trackChanges);
        if (facultyEntity is null)
            throw new DeanForFacultyNotFoundException(deanId);

        var facultyDto = _mapper.Map<FacultyDto>(facultyEntity);
        return facultyDto;
    }
    private async Task CheckIfUniversityExists(Guid universityId, bool trackChanges)
    {
        var university = await _repository.University.GetUniversityAsync(universityId, trackChanges);
        if (university is null)
            throw new UniversityNotFoundException(universityId);
    }

    private async Task<Faculty> GetFacultyForUniversityAndCheckIfItExists
        (Guid universityId, Guid id, bool trackChanges)
    {
        var facultyDb = await _repository.Faculty.GetFacultyAsync(universityId, id, trackChanges);
        if (facultyDb is null)
            throw new FacultyNotFoundException(id);

        return facultyDb;
    }
}

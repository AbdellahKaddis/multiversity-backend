using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using System.ComponentModel.Design;

namespace Service;

public class UniversityService : IUniversityService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;

    public UniversityService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<UniversityDto> CreateUniversityAsync(UniversityForCreationDto university)
    {
        var universityEntity = _mapper.Map<University>(university);

        _repository.University.CreateUniversity(universityEntity);
        await _repository.SaveAsync();

        var universityToReturn = _mapper.Map<UniversityDto>(universityEntity);

        return universityToReturn;
    }

    public async Task DeleteUniversityAsync(Guid universityId, bool trackChanges)
    {
        var university = await GetUniversityAndCheckIfItExists(universityId, trackChanges);

        _repository.University.DeleteUniversity(university);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<UniversityDto>> GetAllUniversitiesAsync(bool trackChanges)
    {
        var universities = await _repository.University.GetAllUniversitiesAsync(trackChanges);

        var universitiesDto = _mapper.Map<IEnumerable<UniversityDto>>(universities);

        return universitiesDto;
    }

    public async Task<UniversityDto> GetUniversityAsync(Guid universityId, bool trackChanges)
    {
        var university = await GetUniversityAndCheckIfItExists(universityId, trackChanges);

        var universityDto = _mapper.Map<UniversityDto>(university);
        return universityDto;
    }

    public async Task UpdateUniversityAsync(Guid universityId, UniversityForUpdateDto universityForUpdate, bool trackChanges)
    {
        var university = await GetUniversityAndCheckIfItExists(universityId, trackChanges);

        _mapper.Map(universityForUpdate, university);
        await _repository.SaveAsync();
    }
    private async Task<University> GetUniversityAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var university = await _repository.University.GetUniversityAsync(id, trackChanges);
        if (university is null)
            throw new UniversityNotFoundException(id);

        return university;
    }
}

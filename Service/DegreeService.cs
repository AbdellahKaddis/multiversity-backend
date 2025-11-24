using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
public class DegreeService : IDegreeService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public DegreeService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<DegreeDto> CreateDegree(Guid universityId,DegreeForCreationDto degree)
    {
        await CheckIfUniversityExists(universityId, false);

        var degreeEntity = _mapper.Map<Degree>(degree);

        _repository.Degree.CreateDegree(universityId, degreeEntity);
        await _repository.SaveAsync();

        var degreeToReturn = _mapper.Map<DegreeDto>(degreeEntity);
        return degreeToReturn;
    }

    public async Task DeleteDegree(Guid degreeId, bool trackChanges)
    {
        var degreeEntity = await GetDegreeAndCheckIfItExists(degreeId, trackChanges);

        _repository.Degree.DeleteDegree(degreeEntity);
        await _repository.SaveAsync();
    }

    public async Task<IEnumerable<DegreeDto>> GetAllDegreesAsync(Guid universityId, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, false);
        var degrees = await _repository.Degree.GetAllDegreesAsync(universityId, trackChanges);
        var degreesDto = _mapper.Map<IEnumerable<DegreeDto>>(degrees);
        return degreesDto;
    }

    public async Task<DegreeDto> GetDegreeAsync(Guid degreeId, bool trackChanges)
    {
        var degree = await GetDegreeAndCheckIfItExists(degreeId, trackChanges);
        var degreeDto = _mapper.Map<DegreeDto>(degree);
        return degreeDto;
    }

    public async Task UpdateDegree(Guid degreeId, DegreeForUpdateDto degreeForUpdateDto, bool trackChanges)
    {
        await CheckIfUniversityExists(degreeForUpdateDto.UniversityId, false);
        var degree = await GetDegreeAndCheckIfItExists(degreeId, trackChanges);
        _mapper.Map(degreeForUpdateDto, degree);
        await _repository.SaveAsync();
    }
    private async Task<Degree> GetDegreeAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var degree = await _repository.Degree.GetDegreeAsync(id, trackChanges);
        if (degree is null)
            throw new DegreeNotFoundException(id);

        return degree;
    }
    private async Task CheckIfUniversityExists(Guid universityId, bool trackChanges)
    {
        var university = await _repository.University.GetUniversityAsync(universityId, trackChanges);
        if (university is null)
            throw new UniversityNotFoundException(universityId);
    }
}

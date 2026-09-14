using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.ComponentModel.Design;

namespace Service;

public class FacultyService : IFacultyService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public FacultyService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
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

        var faculties = await _repository.Faculty.GetFacultiesAsync(universityId, trackChanges).ProjectTo<FacultyDto>(_mapper.ConfigurationProvider).ToListAsync();
        //var facultiesDto = _mapper.Map<IEnumerable<FacultyDto>>(faculties);
        return faculties;
    }

    public async Task<FacultyDto> GetFacultyAsync(Guid universityId, Guid id, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, trackChanges);

        var facultyEntity = await GetFacultyDetailsAsync(universityId, id, trackChanges);

        var facultyDto = _mapper.Map<FacultyDto>(facultyEntity);
        return facultyDto;
    }
    public async Task<FacultyDto> GetFacultyByDeanIdAsync(string deanId, bool trackChanges)
    {
        await CheckIfDeanExists(deanId, trackChanges);

        var facultyEntity = await _repository.Faculty.GetFacultyByDeanIdAsync(deanId, trackChanges);
        if (facultyEntity is null)
            throw new DeanForFacultyNotFoundException(deanId);

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
    public async Task<FacultyDto> GetFacultyDetailsAsync(
    Guid universityId,
    Guid id, bool trackChanges)
    {
        var faculty = await _repository.Faculty
            .GetFaculty(universityId, id, trackChanges)
            .ProjectTo<FacultyDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();

        if (faculty is null)
            throw new FacultyNotFoundException(id);

        return faculty;
    }
    private async Task CheckIfDeanExists(string? deanId, bool trackChanges)
    {

        var dean = await _userManager.FindByIdAsync(deanId);
        if (dean is null)
            throw new DeanNotFoundException(deanId);
    }
    private async Task CheckIfFacultyExists(Guid facultyId, bool trackChanges)
    {

        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }

    private async Task DeactivateUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UserNotFoundException(userId);

        if (!(bool)user.IsActive)
            throw new DeactivateBadRequestException();

        user.IsActive = false;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            _logger.LogError($"Failed to deactivate user {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            throw new UserDeactivationException();
        }

        _logger.LogInfo($"User {userId} was deactivated.");

    }
    private async Task<FacultyDean> GetCurrentFacultyDeanAsync(Guid facultyId, bool trackChanges)
    {
        await CheckIfFacultyExists(facultyId, trackChanges);

        var facultyDeanEntity = await _repository.FacultyDean.GetCurrentFacultyDeanAsync(facultyId, trackChanges);
        if (facultyDeanEntity is null)
            throw new CurrentFacultyDeanNotFoundException();
        return facultyDeanEntity;
    }


    public async Task EndDeanAssignmentAsync(Guid facultyId)
    {
        await using var transaction = await _repository.BeginTransactionAsync();

        try
        {
            var facultyDean = await GetCurrentFacultyDeanAsync(facultyId, true);

            facultyDean.EndDate = DateTime.UtcNow;

            await DeactivateUserAsync(facultyDean.DeanId);

            await _repository.SaveAsync();

            await transaction.CommitAsync();
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
}

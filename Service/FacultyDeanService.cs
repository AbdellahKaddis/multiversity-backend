using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service;

public class FacultyDeanService : IFacultyDeanService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public FacultyDeanService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<FacultyDeanDto> CreateFacultyDeanAsync(FacultyDeanForCreationDto facultyDeanForCreationDto)
    {
        await CheckIfFacultyExists((Guid)facultyDeanForCreationDto.FacultyId, false);

        await CheckIfDeanExists(facultyDeanForCreationDto.DeanId, false);

        var facultyDeanEntity = _mapper.Map<FacultyDean>(facultyDeanForCreationDto);
        facultyDeanEntity.StartDate = DateTime.UtcNow;
        _repository.FacultyDean.CreateFacultyDean(facultyDeanEntity);
        await _repository.SaveAsync();

        var facultyDeanToReturn = _mapper.Map<FacultyDeanDto>(facultyDeanEntity);
        return facultyDeanToReturn;
    }

    public async Task<IEnumerable<FacultyDeanDto>> GetAllFacultyDeansAsync(FacultyDeanParameters facultyDeanParameteres, bool trackChanges)
    {
        if(facultyDeanParameteres.facultyId is not null)
            await CheckIfFacultyExists((Guid)facultyDeanParameteres.facultyId, trackChanges);
        if (facultyDeanParameteres.deanId is not null)
            await CheckIfDeanExists(facultyDeanParameteres.deanId, false);
        var FacultyDeansEntities = await _repository.FacultyDean.GetAllFacultyDeansForFacultyAsync(facultyDeanParameteres, trackChanges);

        var facultyDeansDto = _mapper.Map<IEnumerable<FacultyDeanDto>>(FacultyDeansEntities);
        return facultyDeansDto;
    }

    public async Task<FacultyDeanDto> GetFacultyDeanAsync(Guid id, bool trackChanges)
    {
        var facultyDeanEntity = await GetFacultyDeanAndCheckIfItExists(id, trackChanges);
        var facultyDeanDto = _mapper.Map<FacultyDeanDto>(facultyDeanEntity);
        return facultyDeanDto;
    }

    public async Task RemoveFacultyDeanAsync(Guid id, bool trackChanges)
    {
        var facultyDeanEntity = await GetFacultyDeanAndCheckIfItExists(id, trackChanges);

        _repository.FacultyDean.RemoveFacultyDean(facultyDeanEntity);
        await _repository.SaveAsync();
    }

    public async Task UpdateFacultyDeanAsync(Guid id, FacultyDeanForUpdateDto facultyDeanForUpdateDto, bool trackChanges)
    {

        var facultyDeanEntity = await GetFacultyDeanAndCheckIfItExists(id, trackChanges);

        await CheckIfFacultyExists((Guid)facultyDeanForUpdateDto.FacultyId, false);

        await CheckIfDeanExists(facultyDeanForUpdateDto.DeanId, false);

        facultyDeanEntity.EndDate = DateTime.UtcNow;
        _mapper.Map(facultyDeanForUpdateDto, facultyDeanEntity);

        await _repository.SaveAsync();
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
    private async Task<FacultyDean> GetFacultyDeanAndCheckIfItExists(Guid id, bool trackChanges)
    {
        var facultyDean = await _repository.FacultyDean.GetFacultyDeanAsync(id, trackChanges);
        if (facultyDean is null)
            throw new FacultyDeanNotFoundException(id);

        return facultyDean;
    }
    public async Task<FacultyDeanDto> GetCurrentFacultyDeanAsync(Guid facultyId, bool trackChanges)
    {
        await CheckIfFacultyExists(facultyId, trackChanges);

        var facultyDeanEntity = await _repository.FacultyDean.GetCurrentFacultyDeanAsync(facultyId, trackChanges);
        if (facultyDeanEntity is null)
            throw new CurrentFacultyDeanNotFoundException();
       
        var facultyDeanDto = _mapper.Map<FacultyDeanDto>(facultyDeanEntity);
        return facultyDeanDto;
    }

    private async Task<string> RegisterFacultyDean(FacultyDeanForRegistrationDto deanForRegistrationDto)
    {
        var user = _mapper.Map<User>(deanForRegistrationDto);
        user.UserName = deanForRegistrationDto.Email;
        user.IsActive = true;
        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
            throw new UserCreationBadRequestException(string.Join(", ", result.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRoleAsync(user, "Dean");

        if (!roleResult.Succeeded)
        {
            throw new UserRoleAssignmentBadRequestException(
                string.Join(", ",
                    roleResult.Errors.Select(e => e.Description)));
        }

        return user.Id;
    }
    public async Task CreateAndAssignDeanAsync(Guid facultyId, FacultyDeanForRegistrationDto deanForRegistrationDto)
    {
        await using var transaction =
        await _repository.BeginTransactionAsync();

        try
        {
            await CheckIfFacultyExists(facultyId, false);
            var currentDean =
                await _repository.FacultyDean
                    .GetCurrentFacultyDeanAsync(facultyId, false);

            if (currentDean != null)
                throw new FacultyAlreadyHasDeanException(facultyId);

            var deanId = await RegisterFacultyDean(deanForRegistrationDto);

            var facultyDeanEntity = new FacultyDean
            {
                DeanId = deanId,
                FacultyId = facultyId,
                StartDate = DateTime.UtcNow
            };
            _repository.FacultyDean.CreateFacultyDean(facultyDeanEntity);

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

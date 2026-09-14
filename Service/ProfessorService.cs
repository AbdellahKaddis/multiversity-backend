using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class ProfessorService : IProfessorService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public ProfessorService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    private async Task<string> RegisterProfessor(ProfessorForCreationDto professorForCreationDto)
    {
        var user = _mapper.Map<User>(professorForCreationDto);
        user.UserName = professorForCreationDto.Email;
        user.IsActive = true;

        var result = await _userManager.CreateAsync(user);

        if (!result.Succeeded)
            throw new UserCreationBadRequestException(string.Join(", ", result.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, "Professor");

            if (!roleResult.Succeeded)
            {
                throw new UserRoleAssignmentBadRequestException(
                    string.Join(", ",
                        roleResult.Errors.Select(e => e.Description)));
            }
            return user.Id;
    }
    public async Task<ProfessorDto> CreateProfessorAsync(ProfessorForCreationDto professorForCreationDto)
    {
        await using var transaction =
        await _repository.BeginTransactionAsync();

        try
        {

            await CheckIfFacultyExists(professorForCreationDto.FacultyId, false);

            await CheckIfDepartmentExists(professorForCreationDto.DepartmentId, false);
            var userId = await RegisterProfessor(professorForCreationDto);

            var isDepartmentHasHead = await _repository.Professor.IsDepartmentHasHeadAsync(professorForCreationDto.FacultyId, professorForCreationDto.DepartmentId, false);
            if (isDepartmentHasHead)
                throw new DepartmentAlreadyHasHeadBadRequestException(professorForCreationDto.DepartmentId);

            var professorEntity = _mapper.Map<Professor>(professorForCreationDto);

            _repository.Professor.CreateProfessor(userId, professorEntity);

            await _repository.SaveAsync();

            await transaction.CommitAsync();

            var professorDto = _mapper.Map<ProfessorDto>(professorEntity);
            return professorDto;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task DeleteProfessorAsync(string professorId, bool trackChanges)
    {
        var professorEntity = await GetProfessorAndCheckIfItExists(professorId, trackChanges);

        if ((bool)professorEntity.IsDepartmentHead)
            throw new DepartmentAlreadyHasHeadBadRequestException((Guid)professorEntity.DepartmentId);
        await DeactivateUserAsync(professorId);
    }
    private async Task DeactivateUserAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new UserNotFoundException(userId);

        if (!(bool)user.IsActive)
            throw new DeactivateBadRequestException();

        user.IsActive = false;
        user.DeletedAt = DateTime.UtcNow;

        var result = await _userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            _logger.LogError($"Failed to deactivate user {userId}. Errors: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            throw new UserDeactivationException();
        }

        _logger.LogInfo($"User {userId} was deactivated.");

    }
    public async Task<IEnumerable<ProfessorDto>> GetAllProfessorsAsync(ProfessorParameteres professorParameteres, bool trackChanges)
    {
        var professorEntities = await _repository.Professor.GetProfessorsAsync(professorParameteres, trackChanges);
        var professorsDto = _mapper.Map<IEnumerable<ProfessorDto>>(professorEntities);
        return professorsDto;
    }

    public async Task<ProfessorDto> GetProfessorAsync(string professorId, bool trackChanges)
    {
        var professorEntity = await GetProfessorAndCheckIfItExists(professorId, trackChanges);
        var professorDto = _mapper.Map<ProfessorDto>(professorEntity);
        return professorDto;
    }

    public async Task UpdateProfessorAsync(string professorId, ProfessorForUpdateDto professorForUpdateDto, bool trackChanges)
    {
        await using var transaction =await _repository.BeginTransactionAsync();

        try
        {
            var professorEntity = await GetProfessorAndCheckIfItExists(professorId, trackChanges);

            await CheckIfFacultyExists(professorForUpdateDto.FacultyId, false);

            await CheckIfDepartmentExists(professorForUpdateDto.DepartmentId, false);


            if ((bool)professorEntity.IsDepartmentHead)
                throw new DepartmentAlreadyHasHeadBadRequestException((Guid)professorEntity.DepartmentId);

            _mapper.Map(professorForUpdateDto, professorEntity.User);
            await _userManager.UpdateAsync(professorEntity.User);

            _mapper.Map(professorForUpdateDto, professorEntity);
            await _repository.SaveAsync();

            await transaction.CommitAsync();

        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    private async Task CheckIfFacultyExists(Guid facultyId, bool trackChanges)
    {
        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }
    private async Task CheckIfDepartmentExists(Guid departmentId, bool trackChanges)
    {
        var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
        if (department is null)
            throw new DepartmentNotFoundException(departmentId);
    }
    private async Task<Professor> GetProfessorAndCheckIfItExists(string professorId, bool trackChanges)
    {
        var professor = await _repository.Professor.GetProfessorAsync(professorId, trackChanges);
        if (professor is null)
            throw new ProfessorNotFoundException(professorId);

        return professor;
    }
}

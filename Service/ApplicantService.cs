
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class ApplicantService : IApplicantService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;

    public ApplicantService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    private async Task<Applicant> GetApplicantAndCheckIfExists(string applicantId, bool trackChanges)
    {
        var applicant = await _repository.Applicant.GetApplicantAsync(applicantId, trackChanges);
        if (applicant is null)
            throw new ApplicantNotFoundException(applicantId);

        return applicant;
    }

    public async Task<IEnumerable<ApplicantDto>> GetApplicantsAsync(Guid universityId, ApplicantParameters applicantParameters, bool trackChanges)
    {
        var applicantEntities = await _repository.Applicant.GetApplicantsAsync(universityId, applicantParameters, trackChanges);
        var applicantsDto = _mapper.Map<IEnumerable<ApplicantDto>>(applicantEntities);
        return applicantsDto;
    }

    public async Task<ApplicantDto> GetApplicantAsync(string applicantId, bool trackChanges)
    {
        var applicantEntity = await GetApplicantAndCheckIfExists(applicantId, trackChanges);
        var applicantDto = _mapper.Map<ApplicantDto>(applicantEntity);
        return applicantDto;
    }

    public async Task<string> RegisterApplicant(ApplicantForRegistrationDto applicantForRegistrationDto)
    {
        var user = _mapper.Map<User>(applicantForRegistrationDto);
        user.UserName = applicantForRegistrationDto.Email;
        user.IsActive = true;

        var result = await _userManager.CreateAsync(user, applicantForRegistrationDto.Password);

        if (!result.Succeeded)
            throw new UserCreationBadRequestException(string.Join(", ", result.Errors.Select(e => e.Description)));

        var roleResult = await _userManager.AddToRoleAsync(user, "Applicant");

        if (!roleResult.Succeeded)
        {
            throw new UserRoleAssignmentBadRequestException(
                string.Join(", ",
                    roleResult.Errors.Select(e => e.Description)));
        }
        return user.Id;
    }
    public async Task<ApplicantDto> CreateApplicantAsync(ApplicantForRegistrationDto applicantForRegistrationDto)
    {
        await using var transaction =
        await _repository.BeginTransactionAsync();

        try
        {
            var userId = await RegisterApplicant(applicantForRegistrationDto);

            var applicantEntity = _mapper.Map<Applicant>(applicantForRegistrationDto);

            applicantEntity.Status = "Applicant";
            _repository.Applicant.CreateApplicant(userId, applicantEntity);

            await _repository.SaveAsync();

            await transaction.CommitAsync();

            var applicantDto = _mapper.Map<ApplicantDto>(applicantEntity);
            return applicantDto;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }


}

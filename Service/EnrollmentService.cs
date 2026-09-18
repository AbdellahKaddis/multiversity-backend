

using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class EnrollmentService : IEnrollmentService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    public EnrollmentService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, UserManager<User> userManager)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
    }
    public async Task<EnrollmentDto> CreateEnrollment(EnrollmentForCreationDto enrollmentForCreationDto)
    {
        await using var transaction = await _repository.BeginTransactionAsync();

        try
        {
            await CheckIfFacultExists(enrollmentForCreationDto.FacultyId, false);
            await CheckIfProgramExists(enrollmentForCreationDto.ProgramId, false);

            var applicantEntity = await GetApplicantAndCheckIfExists(
                enrollmentForCreationDto.ApplicantId, true);

            await UpdateUserRoleAsync(enrollmentForCreationDto.ApplicantId, "Student");

            applicantEntity.Status = "Enrolled";

            var enrollmentEntity = _mapper.Map<Enrollment>(enrollmentForCreationDto);
            enrollmentEntity.Status = "Active";
            enrollmentEntity.StudentNumber =
                await _repository.Enrollment.GenerateStudentNumberAsync();

            _repository.Enrollment.CreateEnrollment(enrollmentEntity);

            await _repository.SaveAsync();

            await transaction.CommitAsync();          

            return _mapper.Map<EnrollmentDto>(enrollmentEntity);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }
    public async Task UpdateUserRoleAsync(string userId, string newRole)
    {
        var user = await _userManager.FindByIdAsync(userId)
            ?? throw new UserNotFoundException(userId);

        // 1. Remove all current roles
        var currentRoles = await _userManager.GetRolesAsync(user);
        if (currentRoles.Any())
            await _userManager.RemoveFromRolesAsync(user, currentRoles);

        // 2. Add the new role
        var result = await _userManager.AddToRoleAsync(user, newRole);
        if (!result.Succeeded)
            throw new UserRoleAssignmentBadRequestException(string.Join(", ",result.Errors.Select(e => e.Description)));
    }
    public async Task DeleteEnrollment(Guid enrollmentId, bool trackChanges)
    {
        var enrollmentEntity = await GetEnrollmentAndCheckIfItExistsAsync(enrollmentId, trackChanges);

        _repository.Enrollment.DeleteEnrollment(enrollmentEntity);
        await _repository.SaveAsync();
    }

    public async Task<EnrollmentDto> GetEnrollmentAsync(Guid enrollmentId, bool trackChanges)
    {
        var enrollmentEntity = await GetEnrollmentAndCheckIfItExistsAsync(enrollmentId, trackChanges);
        var enrollmentDto = _mapper.Map<EnrollmentDto>(enrollmentEntity);
        return enrollmentDto;
    }

    public async Task<IEnumerable<EnrollmentDto>> GetEnrollmentsAsync(EnrollmentParameters enrollmentParameters, bool trackChanges)
    {
        var enrollments = await _repository.Enrollment.GetEnrollmentsAsync(enrollmentParameters, trackChanges);
        var enrollmentsDto = _mapper.Map<IEnumerable<EnrollmentDto>>(enrollments);
        return enrollmentsDto;
    }

    public async Task UpdateEnrollment(Guid enrollmentId, EnrollmentForUpdateDto enrollmentForUpdateDto, bool trackChanges)
    {
        await CheckIfFacultExists(enrollmentForUpdateDto.FacultyId, false);
        await CheckIfProgramExists(enrollmentForUpdateDto.ProgramId, false);

        var applicantEntity = await GetApplicantAndCheckIfExists(enrollmentForUpdateDto.ApplicantId, true);

        applicantEntity.Status = enrollmentForUpdateDto.Status == "Active" ? "Enrolled" : enrollmentForUpdateDto.Status;
        var enrollmentEntity = await GetEnrollmentAndCheckIfItExistsAsync(enrollmentId, trackChanges);

        _mapper.Map(enrollmentForUpdateDto, enrollmentEntity);
        await _repository.SaveAsync();
    }

    private async Task CheckIfFacultExists(Guid? facultyId, bool trackChanges)
    {
        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }
    private async Task CheckIfProgramExists(Guid? programId, bool trackChanges)
    {
        var program = await _repository.Program.GetProgramAsync((Guid)programId, trackChanges);
        if (program is null)
            throw new AcademicProgramNotFoundException((Guid)programId);
    }
    private async Task<Applicant> GetApplicantAndCheckIfExists(string applicantId, bool trackChanges)
    {
        var applicant = await _repository.Applicant.GetApplicantAsync(applicantId, trackChanges);
        if (applicant is null)
            throw new ApplicantNotFoundException(applicantId);

        return applicant;
    }

    private async Task<Enrollment> GetEnrollmentAndCheckIfItExistsAsync(Guid enrollmentId, bool trackChanges)
    {
        var enrollmentDb = await _repository.Enrollment.GetEnrollmentAsync(enrollmentId, trackChanges);
        if (enrollmentDb is null)
            throw new EnrollmentNotFoundException(enrollmentId);
        return enrollmentDb;
    }
  
}

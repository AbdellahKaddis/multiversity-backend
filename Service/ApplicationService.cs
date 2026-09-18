
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using static System.Net.Mime.MediaTypeNames;

namespace Service;

public class ApplicationService : IApplicationService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly IFileStorageService _files;

    public ApplicationService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper, IFileStorageService files)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _files = files;
    }
    public async Task<ApplicationDto> CreateApplicationAsync(ApplicationForCreationDto applicationForCreationDto, bool trackChanges)
    {
        await CheckIfProgramExists(applicationForCreationDto.ProgramId, trackChanges);
        await CheckIfFacultExists(applicationForCreationDto.Applicant.FacultyId, trackChanges);
        var applicant = await GetApplicantAndCheckIfExists(applicationForCreationDto.ApplicantId, trackChanges);

        _mapper.Map(applicationForCreationDto.Applicant, applicant);

        var applicationEntity = _mapper.Map<Entities.Models.Application>(applicationForCreationDto);
        applicationEntity.Status = "Submitted";
        applicationEntity.Applicant = applicant;

        _repository.Application.CreateApplication(applicationEntity);

        await _repository.SaveAsync();

        var applicationDto = _mapper.Map<ApplicationDto>(applicationEntity);
        return applicationDto;
    }

    public async Task DeleteApplicationAsync(Guid applicationId, bool trackChanges)
    {
        var applicationEntity = await GetApplicationAndCheckIfItExistsAsync(applicationId, trackChanges);
        _repository.Application.DeleteApplication(applicationEntity);
       await _repository.SaveAsync();
    }

    public async Task<ApplicationDto> GetApplicationAsync(Guid applicationId, bool trackChanges)
    {
        var applicationEntity = await GetApplicationAndCheckIfItExistsAsync(applicationId, trackChanges);

        var applicationDto = _mapper.Map<ApplicationDto>(applicationEntity);
        return applicationDto;
    }

    public async Task<IEnumerable<ApplicationDto>> GetApplicationsAsync(ApplicationParameters applicationParameters, bool trackChanges)
    {
        if(applicationParameters.UniversityId is not null)
            await CheckIfUniversityExists((Guid)applicationParameters.UniversityId, trackChanges);

        if (applicationParameters.FacultyId is not null)
            await CheckIfFacultExists(applicationParameters.FacultyId, trackChanges);

        if (applicationParameters.ProgramId is not null)
            await CheckIfProgramExists(applicationParameters.ProgramId, trackChanges);

        var applicationsEntities = await _repository.Application.GetApplicationsAsync(applicationParameters, trackChanges);
        var applicationDtos = _mapper.Map<IEnumerable<ApplicationDto>>(applicationsEntities);
        return applicationDtos;
    }

    public async Task UpdateApplicationStatusAsync(
        Guid applicationId,
        UpdateApplicationStatusDto dto,
        bool trackChanges)
    {
        var application = await GetApplicationAndCheckIfItExistsAsync(applicationId, trackChanges);

        if (application is null)
            throw new ApplicationNotFoundException(applicationId);

        application.Status = dto.Status;
        if(dto.ReviewerId is not null)
            application.ReviewerId = dto.ReviewerId;

        await _repository.SaveAsync();
    }
    private async Task CheckIfUniversityExists(Guid universityId, bool trackChanges)
    {
        var university = await _repository.University.GetUniversityAsync(universityId, trackChanges);
        if (university is null)
            throw new UniversityNotFoundException(universityId);
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

    private async Task<Entities.Models.Application> GetApplicationAndCheckIfItExistsAsync(Guid applicationId, bool trackChanges)
    {
        var applicationDb = await _repository.Application.GetApplicationAsync(applicationId, trackChanges);
        if (applicationDb is null)
            throw new ApplicationNotFoundException(applicationId);
        return applicationDb;
    }
    
}

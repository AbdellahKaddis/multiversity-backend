
using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Org.BouncyCastle.Asn1.IsisMtt.X509;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;

public class AdmissionService : IAdmissionService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public AdmissionService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<AdmissionDto> CreateAdmissionForProgramAsync(AdmissionForCreationDto admissionForCreationDto, bool trackChanges)
    {
        await CheckIfProgramExists(admissionForCreationDto.ProgramId, trackChanges);
        var admissionEntity = _mapper.Map<Admission>(admissionForCreationDto);

        _repository.Admission.CreateAdmissionForProgram(admissionForCreationDto.ProgramId, admissionEntity);

        await _repository.SaveAsync();

        var admissionDto = _mapper.Map<AdmissionDto>(admissionEntity);
        return admissionDto;
    }

    public async Task DeleteAdmissionForProgramAsync(Guid id, bool trackChanges)
    {
        var admissionEntity = await GetAdmissionAndCheckIfItExists(id, trackChanges);

        foreach(var ar in admissionEntity.Requirements)
        {
            _repository.AdmissionRequirement.DeleteAdmissionRequirement(ar);
        }

        _repository.Admission.DeleteAdmission(admissionEntity);

        await _repository.SaveAsync();
    }


    public async Task<AdmissionDto> GetAdmissionAsync(Guid id, bool trackChanges)
    {
        var admissionEntity = await GetAdmissionAndCheckIfItExists(id, trackChanges);

        var admissionDto = _mapper.Map<AdmissionDto>(admissionEntity);
        return admissionDto;
    }

    public async Task<IEnumerable<AdmissionDto>> GetAdmissionsAsync(Guid universityId, AdmissionParameters admissionParameters, bool trackChanges)
    {
        await CheckIfUniversityExists(universityId, trackChanges);
        if (admissionParameters.FacultyId is not null)
            await CheckIfFacultExists(admissionParameters.FacultyId, trackChanges);

        if (admissionParameters.programId is not  null)
            await CheckIfProgramExists(admissionParameters.programId, trackChanges);
        var admissionEntities = await _repository.Admission.GetAdmissionsAsync(universityId, admissionParameters, trackChanges);
        var admissionDtos = _mapper.Map<IEnumerable<AdmissionDto>>(admissionEntities);
        return admissionDtos;
    }

    public async Task UpdateAdmissionForProgramAsync(Guid id, AdmissionForUpdateDto admissionForUpdateDto, bool progTrackChanges, bool admTrackChanges)
    {
        await CheckIfProgramExists(admissionForUpdateDto.ProgramId, progTrackChanges);
        var admissionEntity = await GetAdmissionAndCheckIfItExists(id, admTrackChanges);

        _mapper.Map(admissionForUpdateDto, admissionEntity);

        // --- Requirements upsert ---
        var incoming = admissionForUpdateDto.Requirements ?? new List<AdmissionRequirementForUpdateDto>();
        var incomingIds = incoming
            .Where(r => r.Id != Guid.Empty)   // adjust to Guid/int
            .Select(r => r.Id)
            .ToHashSet();

        // 1. Delete requirements no longer present
        var toRemove = admissionEntity.Requirements
            .Where(r => !incomingIds.Contains(r.Id))
            .ToList();

        foreach (var r in toRemove)
            _repository.AdmissionRequirement.DeleteAdmissionRequirement(r);

        // 2. Update existing / Add new
        foreach (var arDto in incoming)
        {
            if (arDto.Id != Guid.Empty)   // existing
            {
                var existing = admissionEntity.Requirements
                    .FirstOrDefault(r => r.Id == arDto.Id)
                    ?? throw new KeyNotFoundException(
                        $"Requirement {arDto.Id} does not belong to admission {id}.");

                _mapper.Map(arDto, existing);
            }
            else                          // new
            {
                var newReq = _mapper.Map<AdmissionRequirement>(arDto);
                admissionEntity.Requirements.Add(newReq);
            }
        }

        await _repository.SaveAsync();

    }
    private async Task CheckIfProgramExists(Guid programId, bool trackChanges)
    {
        var programDb = await _repository.Program.GetProgramAsync(programId, trackChanges);
        if (programDb is null)
            throw new AcademicProgramNotFoundException(programId);
    }
    private async Task<Admission> GetAdmissionAndCheckIfItExists(Guid admissionId, bool trackChanges)
    {
        var admissionDb = await _repository.Admission.GetAdmissionAsync(admissionId, trackChanges);
        if (admissionDb is null)
            throw new AdmissionNotFoundException(admissionId);
        return admissionDb;
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

}

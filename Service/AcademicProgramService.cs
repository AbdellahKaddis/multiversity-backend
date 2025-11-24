using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;
using Shared.RequestFeatures;

namespace Service;
public class AcademicProgramService : IAcademicProgramService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public AcademicProgramService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<AcademicProgramDto> CreateProgramForDepatmentAsync(Guid departmentId, AcademicProgramForCreationDto programForCreationDto, bool trackChanges)
    {
        await CheckIfDepartmentExists(departmentId, trackChanges);

        await CheckIfDegreeExists(programForCreationDto.DegreeId, trackChanges);

        var programEntity = _mapper.Map<AcademicProgram>(programForCreationDto);

        _repository.Program.CreateProgramForDepartment(departmentId, programEntity);
        await _repository.SaveAsync();

        var programDto = _mapper.Map<AcademicProgramDto>(programEntity);
        return programDto;
    }

    public async Task DeleteProgramAsync(Guid id, bool trackChanges)
    {
        var programEntity = await GetProgramAndCheckIfItExists(id, trackChanges);

        _repository.Program.DeleteProgram(programEntity);
        await _repository.SaveAsync();
    }

    public async Task<AcademicProgramDto> GetProgramAsync(Guid id, bool trackChanges)
    {
        var programEntity = await GetProgramAndCheckIfItExists(id, trackChanges);

        var programDto = _mapper.Map<AcademicProgramDto>(programEntity);
        return programDto;
    }

    public async Task<IEnumerable<AcademicProgramDto>> GetProgramsAsync(ProgramParameters programParameters, bool trackChanges)
    {
        await CheckIfFacultyExists(programParameters.FacultyId, trackChanges);

        await CheckIfDepartmentExists(programParameters.DepartmentId, trackChanges);

        var programsEntities = await _repository.Program.GetProgramsAsync(programParameters, trackChanges);

        var programsDto = _mapper.Map<IEnumerable<AcademicProgramDto>>(programsEntities);
        return programsDto;
    }

    public async Task UpdateProgramAsync(Guid id, AcademicProgramForUpdateDto programForUpdateDto, bool trackChanges)
    {
        await CheckIfDepartmentExists(programForUpdateDto.DepartmentId, trackChanges);

        await CheckIfDegreeExists(programForUpdateDto.DegreeId, trackChanges);

        var programEntity = await GetProgramAndCheckIfItExists(id, trackChanges);

        _mapper.Map(programForUpdateDto, programEntity);

        await _repository.SaveAsync();
    }
    private async Task CheckIfDepartmentExists(Guid? departmentId, bool trackChanges)
    {
        if (departmentId is null)
            return;

        var department = await _repository.Department.GetDepartmentAsync(departmentId, trackChanges);
        if (department is null)
            throw new DepartmentNotFoundException(departmentId);
    }
    private async Task CheckIfDegreeExists(Guid degreeId, bool trackChanges)
    {
        var degree = await _repository.Degree.GetDegreeAsync(degreeId, trackChanges);
        if (degree is null)
            throw new DegreeNotFoundException(degreeId);
    }
    private async Task CheckIfFacultyExists(Guid? facultyId, bool trackChanges)
    {
        if (facultyId is null)
            return;

        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }

    private async Task<AcademicProgram> GetProgramAndCheckIfItExists
    (Guid id, bool trackChanges)
    {
        var programDb = await _repository.Program.GetProgramAsync(id, trackChanges);
        if (programDb is null)
            throw new AcademicProgramNotFoundException(id);

        return programDb;
    }
}

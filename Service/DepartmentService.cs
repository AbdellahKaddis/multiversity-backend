using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Service.Contracts;
using Shared.DataTransferObjects;

namespace Service;
public class DepartmentService : IDepartmentService
{
    private readonly IRepositoryManager _repository;
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    public DepartmentService(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
    }
    public async Task<DepartmentDto> CreateDepartmentForFacultyAsync(Guid facultyId, DepartmentForCreationDto departmentForCreation, bool trackChanges)
    {
        await CheckIfFcaultyExists(facultyId, trackChanges);

        var departmentEntity = _mapper.Map<Department>(departmentForCreation);

        _repository.Department.CreateDepartmentForFaculty(facultyId, departmentEntity);
        await _repository.SaveAsync();

        var departmentToReturn = _mapper.Map<DepartmentDto>(departmentEntity);
        return departmentToReturn;
    }

    public async Task DeleteDepartmentForFacultyAsync(Guid facultyId, Guid id, bool trackChanges)
    {
        await CheckIfFcaultyExists(facultyId, trackChanges);

        var departmentEntity = await GetDepartmentForFacultyAndCheckIfItExists(facultyId, id, trackChanges);

        _repository.Department.DeleteDepartment(departmentEntity);
        await _repository.SaveAsync();
    }

    public async Task<DepartmentDto> GetDepartmentAsync(Guid facultyId, Guid id, bool trackChanges)
    {
        await CheckIfFcaultyExists(facultyId, trackChanges);

        var departmentEntity = await GetDepartmentForFacultyAndCheckIfItExists(facultyId, id, trackChanges);

        var departmentDto = _mapper.Map<DepartmentDto>(departmentEntity);
        return departmentDto;

    }

    public async Task<IEnumerable<DepartmentDto>> GetDepartmentsAsync(Guid facultyId, bool trackChanges)
    {
        await CheckIfFcaultyExists(facultyId, trackChanges);

        var departments = await _repository.Department.GetDepartmentsAsync(facultyId, trackChanges);
        var departmentsDto = _mapper.Map<IEnumerable<DepartmentDto>>(departments);
        return departmentsDto;
    }

    public async Task UpdateDepartmentForFacultyAsync(Guid facultyId, Guid id, DepartmentForUpdateDto departmentForUpdate, bool facTrackChanges, bool depTrackChanges)
    {
        await CheckIfFcaultyExists(facultyId, facTrackChanges);

        var departmentEntity = await GetDepartmentForFacultyAndCheckIfItExists(facultyId, id, depTrackChanges);

        _mapper.Map(departmentForUpdate, departmentEntity);
        await _repository.SaveAsync();
    }
    private async Task CheckIfFcaultyExists(Guid facultyId, bool trackChanges)
    {
        var faculty = await _repository.Faculty.GetFacultyAsync(facultyId, trackChanges);
        if (faculty is null)
            throw new FacultyNotFoundException(facultyId);
    }

    private async Task<Department> GetDepartmentForFacultyAndCheckIfItExists
        (Guid facultyId, Guid id, bool trackChanges)
    {
        var departmentDb = await _repository.Department.GetDepartmentAsync(facultyId, id, trackChanges);
        if (departmentDb is null)
            throw new DepartmentNotFoundException(id);

        return departmentDb;
    }
}


namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IUniversityRepository University { get; }
        IFacultyRepository Faculty { get; }
        IDepartmentRepository Department { get; }
        IDegreeRepository Degree { get; }
        IAcademicProgramRepository Program { get; }
        ICourseRepository Course { get; }
        IProgramCourseRepository ProgramCourse { get; }
        Task SaveAsync();

    }
}

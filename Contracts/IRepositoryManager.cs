
namespace Contracts
{
    public interface IRepositoryManager
    {
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

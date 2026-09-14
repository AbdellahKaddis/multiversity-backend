
using Microsoft.EntityFrameworkCore.Storage;

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
        IProfessorRepository Professor { get; }
        IFacultyDeanRepository FacultyDean { get; }
        IProfessorCourseRepository ProfessorCourse { get; }
        IAdmissionRepository Admission { get; }
        IAdmissionRequirementRepository AdmissionRequirement { get; }
        Task<IDbContextTransaction> BeginTransactionAsync();
        Task SaveAsync();

    }
}

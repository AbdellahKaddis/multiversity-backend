
namespace Service.Contracts
{
    public interface IServiceManager
    {
        IAuthService AuthenticationService { get; }
        IUniversityService UniversityService { get; }
        IFacultyService FacultyService { get; }
        IDepartmentService DepartmentService { get; }
        IDegreeService DegreeService { get; }
        IAcademicProgramService ProgramService { get; }
        ICourseService CourseService { get; }
        IProgramCourseService ProgramCourseService { get; }
        IProfessorService ProfessorService { get; }
        IFacultyDeanService FacultyDeanService { get;}
        IProfessorCourseService ProfessorCourseService { get; }
        IAdmissionService AdmissionService { get; }
        IApplicantService ApplicantService { get; }
        IApplicationService ApplicationService { get; }
        IEnrollmentService EnrollmentService { get; }
        IGradeService GradeService { get; }

    }
}

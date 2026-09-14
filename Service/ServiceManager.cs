using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Service.Contracts;

namespace Service
{
    public sealed class ServiceManager : IServiceManager
    {
        private readonly Lazy<IAuthService> _authenticationService;
        private readonly Lazy<IUniversityService> _universityService;
        private readonly Lazy<IFacultyService> _facultyService;
        private readonly Lazy<IDepartmentService> _departmentService;
        private readonly Lazy<IDegreeService> _degreeService;
        private readonly Lazy<IAcademicProgramService> _programService;
        private readonly Lazy<ICourseService> _courseService;
        private readonly Lazy<IProgramCourseService> _programCourseService;
        private readonly Lazy<IProfessorService> _professorService;
        private readonly Lazy<IFacultyDeanService> _facultyDeanService;
        private readonly Lazy<IProfessorCourseService> _professorCourseService;
        private readonly Lazy<IAdmissionService> _admissionService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration,IEmailService emailService, IDistributedCache cache, SignInManager<User> signInManager)
        {

            _authenticationService = new Lazy<IAuthService>(() => 
            new AuthService(logger, mapper, userManager, configuration, emailService,cache, signInManager));

            _universityService = new Lazy<IUniversityService>(() =>
            new UniversityService(repositoryManager, logger, mapper));

            _facultyService = new Lazy<IFacultyService>(()=> 
            new FacultyService(repositoryManager,logger, mapper, userManager));

            _departmentService = new Lazy<IDepartmentService>(() =>
            new DepartmentService(repositoryManager, logger, mapper));

            _degreeService = new Lazy<IDegreeService>(() =>
            new DegreeService(repositoryManager, logger, mapper));

            _programService = new Lazy<IAcademicProgramService>(() =>
            new AcademicProgramService(repositoryManager, logger, mapper));

            _courseService = new Lazy<ICourseService>(() =>
            new CourseService(repositoryManager, logger, mapper));

            _programCourseService = new Lazy<IProgramCourseService>(() =>
            new ProgramCourseService(repositoryManager, logger, mapper));
            _professorService = new Lazy<IProfessorService>(() =>
            new ProfessorService(repositoryManager, logger, mapper, userManager));

            _facultyDeanService = new Lazy<IFacultyDeanService>(() =>
           new FacultyDeanService(repositoryManager, logger, mapper, userManager));

            _professorCourseService = new Lazy<IProfessorCourseService>(() =>
              new ProfessorCourseService(repositoryManager, logger, mapper));
            _admissionService = new Lazy<IAdmissionService>(() =>
            new AdmissionService(repositoryManager, logger, mapper));
        }
        
        public IAuthService AuthenticationService => _authenticationService.Value;
        public IUniversityService UniversityService => _universityService.Value;
        public IFacultyService FacultyService => _facultyService.Value;
        public IDepartmentService DepartmentService => _departmentService.Value;
        public IDegreeService DegreeService => _degreeService.Value;
        public IAcademicProgramService ProgramService => _programService.Value;
        public ICourseService CourseService => _courseService.Value;
        public IProgramCourseService ProgramCourseService => _programCourseService.Value;
        public IProfessorService ProfessorService => _professorService.Value;
        public IFacultyDeanService FacultyDeanService => _facultyDeanService.Value;
        public IProfessorCourseService ProfessorCourseService => _professorCourseService.Value;
        public IAdmissionService AdmissionService => _admissionService.Value;
    }
}

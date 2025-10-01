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
        private readonly Lazy<ICompanyService> _companyService;
        private readonly Lazy<IEmployeeService> _employeeService;
        private readonly Lazy<IAuthService> _authenticationService;
        private readonly Lazy<IUniversityService> _universityService;
        private readonly Lazy<IFacultyService> _facultyService;
        public ServiceManager(IRepositoryManager repositoryManager, ILoggerManager
        logger, IMapper mapper, UserManager<User> userManager, IConfiguration configuration,IEmailService emailService, IDistributedCache cache)
        {
            _companyService = new Lazy<ICompanyService>(() => new
            CompanyService(repositoryManager, logger, mapper));

            _employeeService = new Lazy<IEmployeeService>(() => new
            EmployeeService(repositoryManager, logger, mapper));

            _authenticationService = new Lazy<IAuthService>(() => 
            new AuthService(logger, mapper, userManager, configuration, emailService,cache));

            _universityService = new Lazy<IUniversityService>(() =>
            new UniversityService(repositoryManager, logger, mapper));

            _facultyService = new Lazy<IFacultyService>(()=> 
            new FacultyService(repositoryManager,logger, mapper));
        }
        public ICompanyService CompanyService => _companyService.Value;
        public IEmployeeService EmployeeService => _employeeService.Value;
        public IAuthService AuthenticationService => 
            _authenticationService.Value;
        public IUniversityService UniversityService => _universityService.Value;
        public IFacultyService FacultyService => _facultyService.Value;
    }
}

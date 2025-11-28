using Contracts;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IUniversityRepository> _universityRepository;
        private readonly Lazy<IFacultyRepository> _facultyRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IDegreeRepository> _degreeRepository;
        private readonly Lazy<IAcademicProgramRepository> _programRepository;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<IProgramCourseRepository> _programCourseRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new
            CompanyRepository(repositoryContext));

            _employeeRepository = new Lazy<IEmployeeRepository>(() => new
            EmployeeRepository(repositoryContext));

            _universityRepository = new Lazy<IUniversityRepository>(() => new
            UniversityRepository(repositoryContext));

            _facultyRepository = new Lazy<IFacultyRepository>(() => new
            FacultyRepository(repositoryContext));

            _departmentRepository = new Lazy<IDepartmentRepository>(() => new
            DepartmentRepository(repositoryContext));

            _degreeRepository = new Lazy<IDegreeRepository>(() => new
            DegreeRepository(repositoryContext));

            _programRepository = new Lazy<IAcademicProgramRepository>(() => new
            AcademicProgramRepository(repositoryContext));

            _courseRepository = new Lazy<ICourseRepository>(() => new
            CourseRepository(repositoryContext));

            _programCourseRepository = new Lazy<IProgramCourseRepository>(() => new
            ProgramCourseRepository(repositoryContext));
        }
        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;
        public IUniversityRepository University => _universityRepository.Value;
        public IFacultyRepository Faculty => _facultyRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IDegreeRepository Degree => _degreeRepository.Value;
        public IAcademicProgramRepository Program => _programRepository.Value;
        public ICourseRepository Course => _courseRepository.Value;
        public IProgramCourseRepository ProgramCourse => _programCourseRepository.Value;
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}

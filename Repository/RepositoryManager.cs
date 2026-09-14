using Contracts;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public sealed class RepositoryManager : IRepositoryManager
    {
        private readonly RepositoryContext _repositoryContext;
        private readonly Lazy<IUniversityRepository> _universityRepository;
        private readonly Lazy<IFacultyRepository> _facultyRepository;
        private readonly Lazy<IDepartmentRepository> _departmentRepository;
        private readonly Lazy<IDegreeRepository> _degreeRepository;
        private readonly Lazy<IAcademicProgramRepository> _programRepository;
        private readonly Lazy<ICourseRepository> _courseRepository;
        private readonly Lazy<IProgramCourseRepository> _programCourseRepository;
        private readonly Lazy<IProfessorRepository> _professorRepository;
        private readonly Lazy<IFacultyDeanRepository> _facultyDeanRepository;
        private readonly Lazy<IProfessorCourseRepository> _professorCourseRepository;
        private readonly Lazy<IAdmissionRepository> _admissionRepository;
        private readonly Lazy<IAdmissionRequirementRepository> _admissionRequirementRepository;
        public RepositoryManager(RepositoryContext repositoryContext)
        {

            _repositoryContext = repositoryContext;
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
            
            _professorRepository = new Lazy<IProfessorRepository>(() => new 
            ProfessorRepository (repositoryContext));

            _facultyDeanRepository = new Lazy<IFacultyDeanRepository>(() => new 
            FacultyDeanRepository(repositoryContext));

            _professorCourseRepository = new Lazy<IProfessorCourseRepository>(() => new
            ProfessorCourseRepository(repositoryContext));

            _admissionRepository = new Lazy<IAdmissionRepository>(() => new
            AdmissionRepository(repositoryContext));

            _admissionRequirementRepository = new Lazy<IAdmissionRequirementRepository>(() => new
            AdmissionRequirementRepository(repositoryContext));
        }

        public IUniversityRepository University => _universityRepository.Value;
        public IFacultyRepository Faculty => _facultyRepository.Value;
        public IDepartmentRepository Department => _departmentRepository.Value;
        public IDegreeRepository Degree => _degreeRepository.Value;
        public IAcademicProgramRepository Program => _programRepository.Value;
        public ICourseRepository Course => _courseRepository.Value;
        public IProgramCourseRepository ProgramCourse => _programCourseRepository.Value;
        public IProfessorRepository Professor => _professorRepository.Value;
        public IFacultyDeanRepository FacultyDean =>  _facultyDeanRepository.Value;
        public IProfessorCourseRepository ProfessorCourse => _professorCourseRepository.Value;
        public IAdmissionRepository Admission => _admissionRepository.Value;
        public IAdmissionRequirementRepository AdmissionRequirement => _admissionRequirementRepository.Value;

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _repositoryContext.Database.BeginTransactionAsync();
        }
        public async Task SaveAsync() => await _repositoryContext.SaveChangesAsync();

    }
}

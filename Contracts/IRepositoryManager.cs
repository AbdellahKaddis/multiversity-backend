
namespace Contracts
{
    public interface IRepositoryManager
    {
        ICompanyRepository Company { get; }
        IEmployeeRepository Employee { get; }
        IUniversityRepository University { get; }
        IFacultyRepository Faculty { get; }
        Task SaveAsync();

    }
}

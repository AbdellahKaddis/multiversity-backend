

using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IApplicationRepository
{
    Task<IEnumerable<Application>> GetApplicationsAsync(ApplicationParameters applicationParameters, bool trackChanges);
    Task<Application> GetApplicationAsync(Guid id, bool trackChanges);
    void CreateApplication(Application application);
    void DeleteApplication(Application application);

}

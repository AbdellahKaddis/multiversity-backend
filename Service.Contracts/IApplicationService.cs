

using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System.Threading.Tasks;

namespace Service.Contracts;

public interface IApplicationService
{
    Task<IEnumerable<ApplicationDto>> GetApplicationsAsync(ApplicationParameters applicationParameters, bool trackChanges);
    Task<ApplicationDto> GetApplicationAsync(Guid applicationId, bool trackChanges);
    Task<ApplicationDto> CreateApplicationAsync(ApplicationForCreationDto applicationForCreationDto, bool trackChanges);
    Task DeleteApplicationAsync(Guid applicationId, bool trackChanges);
    Task UpdateApplicationStatusAsync(
         Guid applicationId,
         UpdateApplicationStatusDto dto,
         bool trackChanges);
}

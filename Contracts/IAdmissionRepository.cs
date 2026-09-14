

using Entities.Models;
using Shared.RequestFeatures;

namespace Contracts;

public interface IAdmissionRepository
{
    Task<IEnumerable<Admission>> GetAdmissionsAsync(Guid universityId, AdmissionParameters admissionParameters, bool trackChanges);
    Task<Admission> GetAdmissionAsync(Guid id, bool trackChanges);
    void CreateAdmissionForProgram(Guid programId, Admission admission);
    void DeleteAdmission(Admission admission);
}

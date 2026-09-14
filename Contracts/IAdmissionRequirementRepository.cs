using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts;

public interface IAdmissionRequirementRepository
{
    Task<IEnumerable<AdmissionRequirement>> GetAdmissionRequiremetsForAdmissionAsync(Guid admissionId, bool trackChanges);
    Task<AdmissionRequirement> GetAdmissionRequirementAsync(Guid admissionId, Guid id, bool trackChanges);
    void CreateAdmissionRequirementForAdmission(Guid admissionId, AdmissionRequirement admissionRequirement);
    void DeleteAdmissionRequirement(AdmissionRequirement admissionRequirement);
}

using Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class AdmissionRequirementRepository : RepositoryBase<AdmissionRequirement>, IAdmissionRequirementRepository
{
    public AdmissionRequirementRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateAdmissionRequirementForAdmission(Guid admissionId, AdmissionRequirement admissionRequirement)
    {
        admissionRequirement.AdmissionId = admissionId;
        Create(admissionRequirement);
    }

    public void DeleteAdmissionRequirement(AdmissionRequirement admissionRequirement)
    {
        Delete(admissionRequirement);
    }

    public async Task<AdmissionRequirement> GetAdmissionRequirementAsync(Guid admissionId, Guid id, bool trackChanges)
    {
        return await FindByCondition(ar => ar.AdmissionId.Equals(admissionId) && ar.Id.Equals(id), trackChanges).SingleOrDefaultAsync();
    }

    public async Task<IEnumerable<AdmissionRequirement>> GetAdmissionRequiremetsForAdmissionAsync(Guid admissionId, bool trackChanges)
    {
        return await FindByCondition(ar => ar.AdmissionId.Equals(admissionId), trackChanges).ToListAsync();
    }
}

using Contracts;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Extensions.Utility;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository;

public class AdmissionRepository : RepositoryBase<Admission>, IAdmissionRepository
{
    public AdmissionRepository(RepositoryContext repositoryContext) : base(repositoryContext) { }
    public void CreateAdmissionForProgram(Guid programId, Admission admission)
    {
        admission.ProgramId = programId;
        Create(admission);
    }

    public void DeleteAdmission(Admission admission)
    {
        Delete(admission);
    }

    public async Task<Admission> GetAdmissionAsync(Guid id, bool trackChanges)
    {
        return await FindByCondition(a => a.Id.Equals(id), trackChanges)
            .Include(a => a.Program)
             .ThenInclude(p => p.Department)
            .Include(a => a.Requirements.OrderBy(r => r.DisplayOrder))
            .SingleOrDefaultAsync();

    }

    public async Task<IEnumerable<Admission>> GetAdmissionsAsync(Guid universityId, AdmissionParameters admissionParameters, bool trackChanges)
    {
        return await FindByCondition(a => a.Program.Department.Faculty.UniversityId.Equals(universityId), trackChanges)
             .Include(a => a.Program)
             .ThenInclude(p => p.Department)
    .Include(a => a.Requirements.OrderBy(r => r.DisplayOrder))
            .FilterAdmissions(admissionParameters.FacultyId, admissionParameters.programId)
            .ToListAsync();
    }
}

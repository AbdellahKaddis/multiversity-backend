using Shared.DataTransferObjects;
using Shared.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Contracts;

public interface IAdmissionService
{
    Task<IEnumerable<AdmissionDto>> GetAdmissionsAsync(Guid universityId, AdmissionParameters admissionParameters, bool trackChanges);
    Task<AdmissionDto> GetAdmissionAsync(Guid id, bool trackChanges);
    Task<AdmissionDto> CreateAdmissionForProgramAsync(AdmissionForCreationDto
    admissionForCreationDto, bool trackChanges);
    Task DeleteAdmissionForProgramAsync(Guid id, bool trackChanges);
    Task UpdateAdmissionForProgramAsync(Guid id,
    AdmissionForUpdateDto admissionForUpdateDto, bool progTrackChanges, bool
    admTrackChanges);
}

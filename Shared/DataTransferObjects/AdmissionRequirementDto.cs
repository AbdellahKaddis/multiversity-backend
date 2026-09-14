
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record AdmissionRequirementDto(
 Guid Id,
 Guid AdmissionId,
 string? Name,
 ushort DisplayOrder
);


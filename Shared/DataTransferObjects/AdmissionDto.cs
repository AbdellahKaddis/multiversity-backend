using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects;

public record AdmissionDto(
    Guid Id,
    Guid ProgramId,
    string? ProgramName,
    string? Title,
    string? AcademicYear,
    DateTime StartDate,
    DateTime EndDate,
    string? Process,
    Guid FacultyId,
 ICollection<AdmissionRequirementDto> Requirements);

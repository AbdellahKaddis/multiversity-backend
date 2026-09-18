

using System.ComponentModel.DataAnnotations;

namespace Shared.RequestFeatures;

public class EnrollmentParameters : RequestParameters
{
    public string? ApplicantId { get; set; }
    public Guid? ProgramId { get; set; }
    public Guid? UniversityId { get; set; }
    public Guid? FacultyId { get; set; }
    public string? AcademicYear { get; set; }
    public string? StudentNumber { get; set; }
    public string? Status { get; set; }
    public int? YearLevel { get; set; }
}

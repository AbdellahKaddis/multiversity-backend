

namespace Shared.RequestFeatures;

public class ApplicationParameters : RequestParameters
{
    public Guid? UniversityId { get; set; }
    public Guid? FacultyId { get; set; }
    public Guid? ProgramId { get; set; }
    public string? Status { get; set; }
    public string? ApplicantId { get; set; }
}

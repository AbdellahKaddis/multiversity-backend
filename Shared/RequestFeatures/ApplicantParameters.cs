
namespace Shared.RequestFeatures;

public class ApplicantParameters : RequestParameters
{
    public Guid? FacultyId { get; set; }
    public string?  Status { get; set; }
}

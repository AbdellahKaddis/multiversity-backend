namespace Shared.RequestFeatures;

public class FacultyDeanParameters : RequestParameters
{
    public Guid? facultyId { get; set; }
    public string? deanId { get; set; }
}

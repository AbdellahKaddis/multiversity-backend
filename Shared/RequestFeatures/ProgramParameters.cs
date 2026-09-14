namespace Shared.RequestFeatures;
public class ProgramParameters : RequestParameters
{
    public Guid? FacultyId {  get; set; }
    public Guid? DepartmentId { get; set; }
    public Guid? UniversityId { get; set; }
}

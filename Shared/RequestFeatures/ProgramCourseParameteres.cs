namespace Shared.RequestFeatures;

public class ProgramCourseParameteres : RequestParameters
{
    public Guid facultyId {  get; set; }
    public Guid? programId { get; set; }  
}

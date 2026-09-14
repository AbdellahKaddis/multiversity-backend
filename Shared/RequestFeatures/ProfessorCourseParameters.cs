
namespace Shared.RequestFeatures;

public class ProfessorCourseParameters :  RequestParameters
{
    public Guid FacultyId { get; set; }
    public string? ProfessorId { get; set; }
    public Guid? CourseId { get; set; }
}

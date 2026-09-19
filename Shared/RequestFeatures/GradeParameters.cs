namespace Shared.RequestFeatures;

public class GradeParameters : RequestParameters
{
    public Guid? EnrollmentId { get; set; }
    public Guid? CourseId { get; set; }
    public string? AcademicYear { get; set; }
    public string? Semester { get; set; }
    public string? Session { get; set; }
    public bool? IsPublished { get; set; }
    public bool? Validated { get; set; }
}
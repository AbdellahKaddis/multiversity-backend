namespace Shared.DataTransferObjects;

public class SemesterResultDto
{
    public string? Semester { get; set; }
    public decimal? Average { get; set; }
    public int CreditsEarned { get; set; }
    public int CoursesPassed { get; set; }
    public int TotalCourses { get; set; }
    public string Decision { get; set; } = "—";   // "Passed" | "Failed" | "—"

    public List<CourseGradeDto> Courses { get; set; } = new();
}
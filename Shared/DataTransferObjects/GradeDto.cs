namespace Shared.DataTransferObjects;

public class GradeDto
{
    public Guid Id { get; set; }
    public Guid? EnrollmentId { get; set; }
    public string? StudentFullName { get; set; }
    public Guid? CourseId { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public string? ProgramName { get; set; }
    public string? AcademicYear { get; set; }
    public string? Semester { get; set; }
    public string? Session { get; set; }
    public decimal? Score { get; set; }
    public bool? Validated { get; set; }
    public bool? IsPublished { get; set; }
    public uint? Coefficient { get; set; }
    public uint? Credits { get; set; }
    public string? GradedByProfessorId { get; set; }
    public string? ProfessorFullName { get; set; }
    public DateTime GradedAt { get; set; }
}
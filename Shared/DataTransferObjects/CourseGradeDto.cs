namespace Shared.DataTransferObjects;

public class CourseGradeDto
{
    public Guid CourseId { get; set; }
    public string? CourseCode { get; set; }
    public string? CourseName { get; set; }
    public uint Coefficient { get; set; }
    public uint Credits { get; set; }

    public decimal? NormalScore { get; set; }
    public decimal? RattrapageScore { get; set; }
    public decimal? EffectiveScore { get; set; }

    public bool HasNormal { get; set; }
    public bool HasRattrapage { get; set; }
    public bool NeedsRattrapage { get; set; }
    public bool HasFinal { get; set; }

    public bool Validated { get; set; }
    public string? ValidatedBy { get; set; }   // "Normale" | "Rattrapage" | "Compensation" | null
}
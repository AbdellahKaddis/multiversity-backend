using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class Application
{
    [Column("ApplicationId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "ApplicantId is a required field.")]
    public string? ApplicantId { get; set; }
    public Applicant Applicant { get; set; } = null!;

    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid? ProgramId { get; set; }
    public AcademicProgram Program { get; set; } = null!;

    [Required(ErrorMessage = "Grade is a required field.")]
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 20, ErrorMessage = "Grade must be between 0 and 20.")]
    public decimal? Grade { get; set; }

    [MaxLength(20, ErrorMessage = "Maximum length for the BacSerie is 50 characters.")]
    public string? BacSerie { get; set; }

    [Required(ErrorMessage = "BacYear is a required field.")]
    public uint? BacYear { get; set; }

    [MaxLength(30, ErrorMessage = "Maximum length for the Bac Mention is 50 characters.")]
    public string? BacMention { get; set; }

    [Required(ErrorMessage = "Status is a required field.")]
    public string? Status { get; set; }
    public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

    public string? ReviewerId { get; set; }
    public User? Reviewer { get; set; }

    [Required(ErrorMessage = "FileUrl is a required field.")]
    public string? FileUrl { get; set; }
}
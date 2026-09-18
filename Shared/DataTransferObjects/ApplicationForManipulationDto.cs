

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;

public record ApplicationForManipulationDto
{

    [Required(ErrorMessage = "ApplicantId is a required field.")]
    public string? ApplicantId { get; set; }

    [Required(ErrorMessage = "ProgramId is a required field.")]
    public Guid? ProgramId { get; init; }

    [Required(ErrorMessage = "Grade is a required field.")]
    [Column(TypeName = "decimal(5,2)")]
    [Range(0, 20, ErrorMessage = "Grade must be between 0 and 20.")]
    public decimal? Grade { get; init; }

    [Required(ErrorMessage = "BacSerie is required")]
    [MaxLength(20, ErrorMessage = "Maximum length for the BacSerie is 50 characters.")]
    public string? BacSerie { get; init; }

    [Required(ErrorMessage = "BacYear is a required field.")]
    public uint? BacYear { get; init; }

    [Required(ErrorMessage = "BacMention is required")]
    [MaxLength(30, ErrorMessage = "Maximum length for the Bac Mention is 50 characters.")]
    public string? BacMention { get; init; }

    [Required(ErrorMessage = "FileUrl is a required field.")]
    public string? FileUrl { get; init; }
}


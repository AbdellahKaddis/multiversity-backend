using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record CourseForManipulationDto
{
    [Required(ErrorMessage = "Code is a required field."), MaxLength(10)]
    public string? Code { get; init; }

    [Required(ErrorMessage = "Title is a required field."), MaxLength(150)]
    public string? Title { get; init; }

    [MaxLength(1000)]
    public string? Description { get; init; }

    [Required(ErrorMessage = "Coefficient is a required field.")]
    [Range(1, int.MaxValue)]
    public uint Coefficient { get; init; }

    [Required(ErrorMessage = "Credits is a required field.")]
    [Range(1, 10, ErrorMessage = "Credits must be between 1 and 10.")]
    public uint Credits { get; init; }

    public uint? HoursCM { get; init; }
    public uint? HoursTD { get; init; }
    public uint? HoursTP { get; init; }
}

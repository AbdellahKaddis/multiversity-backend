using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class FacultyDean
{
    [Column("FacultyDeanId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "DeanId is a required field.")]
    public string? DeanId { get; set; }
    public User Dean { get; set; }

    [Required(ErrorMessage = "FacultyId is a required field.")]
    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; }

    [Required(ErrorMessage = "StartDate is a required field.")]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
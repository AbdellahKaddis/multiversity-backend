using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;
public class Faculty
{
    [Column("FacultyId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Code is a required field.")]
    public string? Code { get; set; }

    [Required(ErrorMessage = "Type is a required field.")]
    public string? Type { get; set; }

    [Required(ErrorMessage = "Region is a required field.")]
    public string? Region { get; set; }

    [Required(ErrorMessage = "City is a required field.")]
    public string? City { get; set; }

    [Required(ErrorMessage = "Address is a required field.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Email is a required field.")]
    [EmailAddress]
    public string? Email { get; set; }

    [Phone]
    public string? PhoneNumber { get; set; }
    public int? EstablishedYear { get; set; }

    [Required(ErrorMessage = "UniversityId is a required field.")]
    public Guid UniversityId { get; set; }
    public University University { get; set; }
    public string? DeanId { get; set; }
    public User Dean { get; set; }
}


using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models;

public class University
{
    [Column("UniversityId")]
    public Guid Id { get; set; }
    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; set; }
    public string? Abbreviation { get; set; }

    [Required(ErrorMessage = "Type is a required field.")]
    public string? Type { get; set; }
    public string? City { get; set; }
    public string? Address { get; set; }

    [Required(ErrorMessage = "Email is a required field.")]
    [EmailAddress]
    public string? Email { get; set; }
    [Phone]
    public string? PhoneNumber { get; set; }
    public int? YearEstablished { get; set;}
    public string? LogoUrl { get; set; }
    public string? Description { get; set;}
}


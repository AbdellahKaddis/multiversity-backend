

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml;

namespace Entities.Models;
public class Applicant
{
    [Column("ApplicantId")]
    public string Id { get; set; }              

    public User User { get; set; }

    //[Required(ErrorMessage = "DOB is a required field.")]
    public DateTime? DOB { get; set; }

    //[Required(ErrorMessage = "PlaceOfBirth is a required field.")]
    public string? PlaceOfBirth { get; set; }

    //[Required(ErrorMessage = "Gender is a required field.")]
    public string? Gender { get; set; }

    //[Required(ErrorMessage = "CIN is a required field.")]
    public string? CIN { get; set; }

    //[Required(ErrorMessage = "MassarCode is a required field.")]
    public string? MassarCode { get; set; }

    //[Required(ErrorMessage = "Phone is a required field.")]
    public string? Phone { get; set; }

    //[Required(ErrorMessage = "PhotoUrl is a required field.")]
    public string? PhotoUrl { get; set; }

    //[Required(ErrorMessage = "Status is a required field.")]
    public string? Status { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    //[Required(ErrorMessage = "Faculty ID is a required field.")]
    public Guid? FacultyId { get; set; }
    public Faculty Faculty { get; set; }
    public ICollection<Application>? Applications { get; set; }
    public ICollection<Enrollment>? Enrollments { get; set; }
}
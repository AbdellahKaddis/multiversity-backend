using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class AdmissionRequirement
{
    [Column("AdmissionRequirementId")]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "AdmissionId is a required field.")]
    public Guid AdmissionId { get; set; }
    public Admission Admission { get; set; }

    [Required(ErrorMessage = "Name is a required field.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "DisplayOrder is a required field.")]
    [Range(1, int.MaxValue)]
    public ushort DisplayOrder { get; set; }
}
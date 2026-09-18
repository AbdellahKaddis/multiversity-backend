using System;
using System.ComponentModel.DataAnnotations;


namespace Shared.DataTransferObjects;

public record UpdateApplicationStatusDto
{
    [Required(ErrorMessage = "Status is required.")]
    [RegularExpression("^(Submitted|UnderReview|Waitlisted|Approved|Rejected|Accepted|Declined)$",
        ErrorMessage = "Invalid status value.")]
    public string? Status { get; init; } = null!;
    public string? ReviewerId { get; init; }
}

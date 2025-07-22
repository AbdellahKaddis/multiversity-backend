using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record ConfirmVerificationDto(
    [Required, EmailAddress] string Email,
    [Required, RegularExpression(@"^\d{6}$", ErrorMessage = "The value must be exactly 6 digits.")] string Code);
using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record ResetPasswordDto(
    [Required, EmailAddress] string Email, [Required] string Token, [Required] string NewPassword);
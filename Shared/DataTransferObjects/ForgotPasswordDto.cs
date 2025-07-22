using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record ForgotPasswordDto(
    [Required, EmailAddress] string Email);
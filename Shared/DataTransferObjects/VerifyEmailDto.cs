using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObjects;
public record VerifyEmailDto(
    [Required, EmailAddress] string Email);

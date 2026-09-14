
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAuthService
{
     enum AuthenticationResult
    {
        Success,
        InvalidCredentials,
        Inactive,
        LockedOut
    }
    Task<(IdentityResult Result, string? UserId)> RegisterUniversityAdmin(UniversityAdminForRegistrationDto dto);

    Task<(IdentityResult Result, string? UserId)> RegisterFacultyDean(FacultyDeanForRegistrationDto dto);
    Task<AuthenticationResult> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
    Task<bool> CheckEmailExistsAsync(string email);
    Task VerifyEmail(VerifyEmailDto dto);
    Task ConfirmVerification(ConfirmVerificationDto dto);
    Task ForgotPassword(ForgotPasswordDto dto);
    Task<IdentityResult> ResetPassword(ResetPasswordDto dto);
    Task<(IdentityResult Result, string? UserId)> RegisterProfessor(ProfessorForCreationDto professorForCreationDto);
    Task DeactivateUserAsync(string userId);
}

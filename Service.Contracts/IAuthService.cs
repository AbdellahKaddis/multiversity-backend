
using Microsoft.AspNetCore.Identity;
using Shared.DataTransferObjects;

namespace Service.Contracts;

public interface IAuthService
{
    Task<IdentityResult> RegisterUniversityAdmin(UniversityAdminForRegistrationDto dto);
    Task<bool> ValidateUser(UserForAuthenticationDto userForAuth);
    Task<string> CreateToken();
    Task<bool> CheckEmailExistsAsync(string email);
    Task VerifyEmail(VerifyEmailDto dto);
    Task ConfirmVerification(ConfirmVerificationDto dto);
    Task ForgotPassword(ForgotPasswordDto dto);
    Task<IdentityResult> ResetPassword(ResetPasswordDto dto);
}

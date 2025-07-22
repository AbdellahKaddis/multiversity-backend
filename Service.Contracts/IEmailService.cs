namespace Service.Contracts;

public interface IEmailService
{
    Task SendVerificationCodeAsync(string email, string code);
    Task SendPasswordResetEmailAsync(string email, string resetLink);
}

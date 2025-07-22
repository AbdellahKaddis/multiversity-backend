using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System.Net.Mail;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.Text;
using Service.Contracts;


namespace Service;
public sealed class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendVerificationCodeAsync(string email, string code)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_configuration["Smtp:Username"]));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Verify Email Address";

        var builder = new BodyBuilder();

        // HTML Template
        builder.HtmlBody = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Email Verification</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}

        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            background-color: #F9F9F9;
            color: #2C3E50;
            line-height: 1.6;
            padding: 20px;
        }}

        .container {{
            max-width: 600px;
            margin: 0 auto;
            background: #ffffff;
            border-radius: 8px;
            box-shadow: 0 4px 10px rgba(44, 62, 80, 0.05);
            padding: 40px 30px;
        }}

        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}

        .header h2 {{
            font-size: 24px;
            font-weight: 600;
            color: #2C3E50;
        }}

        .otp-display {{
            font-size: 32px;
            letter-spacing: 6px;
            font-weight: 600;
            color: #2C3E50;
            background: #ECF0F1;
            text-align: center;
            padding: 20px;
            margin: 20px 0;
            border-radius: 6px;
            border: 1px solid #BDC3C7;
        }}

        .content p {{
            font-size: 16px;
            margin-bottom: 20px;
            color: #34495E;
        }}

        .warning {{
            font-size: 14px;
            color: #7F8C8D;
            background: #FFF5EC;
            padding: 15px;
            border-radius: 4px;
            margin-top: 20px;
            border-left: 4px solid #E67E22;
        }}

        .warning strong {{
            color: #2C3E50;
        }}

        .footer {{
            text-align: center;
            margin-top: 30px;
            font-size: 12px;
            color: #95A5A6;
        }}

        @media screen and (max-width: 600px) {{
            .container {{
                padding: 20px;
            }}

            .header h2 {{
                font-size: 20px;
            }}

            .otp-display {{
                font-size: 24px;
                letter-spacing: 4px;
            }}

            .content p {{
                font-size: 14px;
            }}

            .warning {{
                font-size: 13px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h2>Verify Your Email Address</h2>
        </div>
        <div class=""content"">
            <p>Please use the code below to verify your email address:</p>
            <div class=""otp-display"">{code}</div>
            <p class=""warning"">
                <strong>Important:</strong> This code is valid for 10 minutes. Do not share this code with anyone.
            </p>
        </div>
        <div class=""footer"">
            <p>© 2025 MultiVersity. All rights reserved.</p>
        </div>
    </div>
</body>
</html>

         ";

        // Plain text fallback
        builder.TextBody = $"Your code is: {code}\nValid for 10 minutes. Do not share this code.";

        message.Body = builder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();
        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true; // Dev only

        await smtp.ConnectAsync(
            _configuration["Smtp:Host"],
            int.Parse(_configuration["Smtp:Port"]),
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _configuration["Smtp:Username"],
            _configuration["Smtp:Password"]);

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
    public async Task SendPasswordResetEmailAsync(string email, string resetLink)
    {
        var message = new MimeMessage();
        message.From.Add(MailboxAddress.Parse(_configuration["Smtp:Username"]));
        message.To.Add(MailboxAddress.Parse(email));
        message.Subject = "Password Reset Request";


        var builder = new BodyBuilder();

        builder.HtmlBody = $@"
<!DOCTYPE html>
<html lang=""en"">
<head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0""/>
    <title>Password Reset Request</title>
    <style>
        * {{
            margin: 0;
            padding: 0;
            box-sizing: border-box;
        }}

        body {{
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif;
            background-color: #f2f4f8;
            color: #2c3e50;
            line-height: 1.6;
            padding: 20px;
        }}

        .container {{
            max-width: 600px;
            margin: 0 auto;
            background: #ffffff;
            border-radius: 10px;
            box-shadow: 0 4px 12px rgba(0, 0, 0, 0.06);
            padding: 40px 30px;
        }}

        .header {{
            text-align: center;
            margin-bottom: 30px;
        }}

        .header h2 {{
            font-size: 24px;
            font-weight: 700;
            color: #1b3a57;
        }}

        .content p {{
            font-size: 16px;
            margin-bottom: 20px;
            color: #34495e;
        }}

        .button {{
            display: inline-block;
            padding: 14px 28px;
            background-color: #f39c12; /* Orange CTA */
            color: #ffffff !important;
            text-decoration: none;
            border-radius: 6px;
            font-size: 16px;
            font-weight: 600;
            text-align: center;
            transition: background-color 0.3s ease;
            margin: 20px 0;
        }}

        .button:hover {{
            background-color: #e67e22;
        }}

        .warning {{
            font-size: 14px;
            color: #555;
            background: #f9fafc;
            padding: 15px;
            border-radius: 6px;
            margin-top: 20px;
            border-left: 4px solid #f39c12;
        }}

        .warning strong {{
            color: #2c3e50;
        }}

        .footer {{
            text-align: center;
            margin-top: 30px;
            font-size: 13px;
            color: #7f8c8d;
        }}

        @media screen and (max-width: 600px) {{
            .container {{
                padding: 20px;
            }}

            .header h2 {{
                font-size: 20px;
            }}

            .content p {{
                font-size: 14px;
            }}

            .button {{
                display: block;
                width: 100%;
                padding: 12px;
            }}

            .warning {{
                font-size: 13px;
            }}
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <div class=""header"">
            <h2>Password Reset Request</h2>
        </div>
        <div class=""content"">
            <p>We received a request to reset your password. Click the button below to proceed with resetting your password:</p>
            <a href=""{resetLink}"" class=""button"">Reset Your Password</a>
            <p class=""warning"">
                <strong>Important:</strong> This link will expire in 10 minutes. If you didn’t request this password reset, you can safely ignore this email.
            </p>
            <p class=""warning"">
                For your security, never share this link with anyone. Our support team will never ask for it.
            </p>
        </div>
        <div class=""footer"">
            <p>&copy; 2025 MultiVersity. All rights reserved.</p>
        </div>
    </div>
</body>
</html>

";

        // Plain text fallback
        builder.TextBody = $"To reset your password, please visit this link:\n\n{resetLink}\n\n" +
                          "This link will expire in 10 minutes. If you didn't request a password reset, " +
                          "you can safely ignore this email.";

        message.Body = builder.ToMessageBody();

        using var smtp = new MailKit.Net.Smtp.SmtpClient();

        // Only disable certificate validation in development
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
        }

        await smtp.ConnectAsync(
            _configuration["Smtp:Host"],
            int.Parse(_configuration["Smtp:Port"]),
            SecureSocketOptions.StartTls);

        await smtp.AuthenticateAsync(
            _configuration["Smtp:Username"],
            _configuration["Smtp:Password"]);

        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);

    }
}

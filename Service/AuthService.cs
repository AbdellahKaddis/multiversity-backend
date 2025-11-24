using AutoMapper;
using Contracts;
using Entities.Exceptions;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Service.Contracts;
using Service.Utilities;
using Shared.DataTransferObjects;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;
using System.Xml.XPath;



namespace Service;

public class AuthService : IAuthService
{
    private readonly ILoggerManager _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IEmailService _emailService;
    private readonly IDistributedCache _cache;
    private User? _user;
    public AuthService(ILoggerManager logger, IMapper mapper,
    UserManager<User> userManager, IConfiguration configuration, IEmailService emailService, IDistributedCache cache)
    {
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _configuration = configuration;
        _emailService = emailService;
        _cache = cache;
    }
    public async Task<(IdentityResult Result, string? UserId)> RegisterUniversityAdmin(UniversityAdminForRegistrationDto
        UniversityAdminForRegistrationDto)
    {
        var user = _mapper.Map<User>(UniversityAdminForRegistrationDto);
        user.UserName = UniversityAdminForRegistrationDto.Email;
        user.EmailConfirmed = true;
        var result = await _userManager.CreateAsync(user, UniversityAdminForRegistrationDto.Password);

        string role = "UniversityAdmin";

        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
            return (result, user.Id);
        }

        return (result, null);
    }
    public async Task<bool> ValidateUser(UserForAuthenticationDto userForAuth)
    {
        _user = await _userManager.FindByEmailAsync(userForAuth.Email);
        var result = (_user != null && await _userManager.CheckPasswordAsync(_user,
       userForAuth.Password));
        if (!result)
            _logger.LogWarn($"{nameof(ValidateUser)}: Authentication failed. Wrong user name or password.");
        return result;
    }
    public async Task<string> CreateToken()
    {
        var signingCredentials = GetSigningCredentials();
        var claims = await GetClaims();
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
    public async Task<bool> CheckEmailExistsAsync(string email)
    {
        return await _userManager.Users
            .AnyAsync(u => u.NormalizedEmail == _userManager.NormalizeEmail(email));
    }
    public async Task VerifyEmail(VerifyEmailDto dto)
    {
        var code = OtpGenerator.GenerateOtp();

        var cacheKey = $"verify-email:{dto.Email}";

        var codeBytes = Encoding.UTF8.GetBytes(code);

        await _cache.SetAsync(cacheKey, codeBytes, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });

        await _emailService.SendVerificationCodeAsync(dto.Email, code);
    }
    public async Task ConfirmVerification(ConfirmVerificationDto dto)
    {
        // 1. Check if email exists in cache
        var cacheKey = $"verify-email:{dto.Email}";
        var cachedBytes = await _cache.GetAsync(cacheKey);

        if (cachedBytes == null)
            throw new CodeNotFoundException();

        var cachedCode = Encoding.UTF8.GetString(cachedBytes);

        // 2. Secure comparison (time-constant to prevent timing attacks)
        if (!CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(cachedCode),
            Encoding.UTF8.GetBytes(dto.Code)))
        {
            throw new InvalidCodeBadRequestException();
        }

        // 3. Clean up used code
        await _cache.RemoveAsync(cacheKey);
    }
    public async Task ForgotPassword(ForgotPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

            var resetLink = $"http://localhost:5173/reset-password?" +
                           $"email={Uri.EscapeDataString(user.Email)}&" +
                           $"token={encodedToken}";

            await _emailService.SendPasswordResetEmailAsync(dto.Email, resetLink);
        }
    }
    public async Task<IdentityResult> ResetPassword(ResetPasswordDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null)
        {
            // Don't reveal that the user does not exist
            return IdentityResult.Success;
        }

        var decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(dto.Token));

        var result = await _userManager.ResetPasswordAsync(user, decodedToken, dto.NewPassword);
        if (result.Succeeded)
            _logger.LogInfo($"Password reset for user {dto.Email}");

        return result;
    }
    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("SECRET"));
        var secret = new SymmetricSecurityKey(key);
        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private async Task<List<Claim>> GetClaims()
    {
        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, _user.Id),
            new Claim("name", $"{_user.FirstName} {_user.LastName}")
        };
        var roles = await _userManager.GetRolesAsync(_user);
        foreach (var role in roles)
        {
            claims.Add(new Claim("role", role));
        }
        return claims;
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials,
    List<Claim> claims)
    {
        var jwtSettings = _configuration.GetSection("JwtSettings");
        var tokenOptions = new JwtSecurityToken
        (
        issuer: jwtSettings["validIssuer"],
        audience: jwtSettings["validAudience"],
        claims: claims,
        expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["expires"])),
        signingCredentials: signingCredentials
        );
        return tokenOptions;
    }

    public async Task<(IdentityResult Result, string? UserId)> RegisterFacultyDean(FacultyDeanForRegistrationDto deanForRegistrationDto)
    {
        var user = _mapper.Map<User>(deanForRegistrationDto);
        user.UserName = deanForRegistrationDto.Email;

        var result = await _userManager.CreateAsync(user);

        string role = "Dean";
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, role);
            return (result, user.Id);
        }
        return (result, null);
    }
}

using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using Shared.DataTransferObjects;
using MultiVersity.Presentation.ActionFilters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiVersity.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IServiceManager _service;
        public AuthController(IServiceManager service) => _service = service;
        
        [HttpPost("register/university-admin")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterUniversityAdmin([FromBody] UniversityAdminForRegistrationDto
            universtyAdminForRegistration)
        {
            var (result, userId) = await
            _service.AuthenticationService.RegisterUniversityAdmin(universtyAdminForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201,new {adminId = userId});
        }

        [HttpPost("register/faculty-dean")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> RegisterFacultyDean([FromBody] FacultyDeanForRegistrationDto
            facultyDeanForRegistration)
        {
            var (result, userId) = await
            _service.AuthenticationService.RegisterFacultyDean(facultyDeanForRegistration);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return StatusCode(201, new { deanId = userId });
        }

        [HttpPost("login")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Authenticate([FromBody] UserForAuthenticationDto
            user)
        {
            if (!await _service.AuthenticationService.ValidateUser(user))
                return Unauthorized();
            return Ok(new
            {
                Token = await _service.AuthenticationService.CreateToken()
            });
        }

        [HttpGet("check-email/{email}")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var exists = await _service.AuthenticationService.CheckEmailExistsAsync(email);

            return Ok(new { exists });
        }

        [HttpPost("verify-email")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
        {
            await _service.AuthenticationService.VerifyEmail(dto);
            return Ok(new { Message = "Verification email sent" });
        }

        [HttpPost("confirm-verification")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ConfirmVerification([FromBody] ConfirmVerificationDto dto)
        {
            await _service.AuthenticationService.ConfirmVerification(dto);
            return Ok(new { Success = true });
        }

        [HttpPost("forgot-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto dto)
        {
            await _service.AuthenticationService.ForgotPassword(dto);
            return Ok(new { message = "If your email is registered, you'll receive a password reset link." });
        }

        [HttpPost("reset-password")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> ResetPassword(ResetPasswordDto dto)
        {
            var result = await _service.AuthenticationService.ResetPassword(dto);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok(new { message = "Password reset successfully." });
        }
    }
}

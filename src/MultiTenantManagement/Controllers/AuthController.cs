using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MultiTenantManagement.Abstractions.Models.Dto;
using MultiTenantManagement.Abstractions.Models.Entities;
using MultiTenantManagement.Abstractions.Services;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IIdentityService identityService;
        private readonly IEmailService emailService;

        public AuthController(UserManager<ApplicationUser> userManager, IIdentityService identityService, IEmailService emailService, ILogger<AuthController> logger)
        {
            this.userManager = userManager;
            this.emailService = emailService;
            this.identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var loginResponse = await identityService.LoginAsync(loginRequest);

            if (loginResponse != null)
                return Ok(loginResponse);
            else
                return BadRequest();
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequest)
        {
            var response = await identityService.RefreshTokenAsync(refreshTokenRequest);

            if (response != null)
                return Ok(response);
            else
                return BadRequest();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            var registerResponse = await identityService.RegisterAsync(registerRequest);

            if (registerResponse.IsSuccess)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(registerResponse.User!);
                var callbackUrl = Url.RouteUrl("ConfirmEmail", new { userId = registerResponse.User!.Id, code = code }, HttpContext.Request.Scheme);

                //TODO: Generate a email template
                //TODO: To set the URL of a front-end page which will then call the ConfirmEmail webApi and then redirect to the login
                await emailService.SendEmailAsync("MultiTenant - Email Confirm", $"Confirm your account by clicking <a href='{callbackUrl}'>here</a>", new List<string>() { registerRequest.Email });
            }

            return StatusCode(registerResponse.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                registerResponse);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto resetPasswordRequest)
        {
            var user = await userManager.FindByEmailAsync(resetPasswordRequest.Email);

            if (user == null)
            {
                return NotFound("User not found");
            }

            if(!(await userManager.IsEmailConfirmedAsync(user)))
            {
                return BadRequest("Unconfirmed registration email");
            }

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            //TODO: To set the URL of a front-end page which will then call the ResetPassword passing new password webApi and then redirect to the login
            var callbackUrl = Url.RouteUrl("ResetPassword", new { userId = user.Id, token }, HttpContext.Request.Scheme);

            await emailService.SendEmailAsync("MultiTenant - Reset Password", $"Reset you password by clicking <a href='{callbackUrl}'>here</a>", new List<string>() { resetPasswordRequest.Email! });

            return Ok(callbackUrl);
        }

        [HttpGet("confirm-email", Name = "ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail([Required]string userId, [Required]string code)
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await userManager.ConfirmEmailAsync(user, code);

            return StatusCode(result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, result);
        }

        [HttpPost("reset-password", Name = "ResetPassword")]
        public async Task<IActionResult> ConfirmEmail(ResetPasswordDto resetPasswordRequest)
        {
            //Map response to a dto object

            var user = await userManager.FindByIdAsync(resetPasswordRequest.UserId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordRequest.Token, resetPasswordRequest.Password);
            
            if (result.Succeeded)
            {
                return Ok();
            }

            return BadRequest(result.Errors);
        }
    }
}
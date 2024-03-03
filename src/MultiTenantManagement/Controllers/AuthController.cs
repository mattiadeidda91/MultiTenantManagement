using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.ForgotPassword;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Login;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Register;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.ResetPassword;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Token;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Template;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace MultiTenantManagement.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> logger;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IIdentityService identityService;
        private readonly IEmailService emailService;

        public AuthController(UserManager<ApplicationUser> userManager, IIdentityService identityService, IEmailService emailService, ILogger<AuthController> logger)
        {
            this.logger = logger;
            this.userManager = userManager;
            this.emailService = emailService;
            this.identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            var loginResponse = await identityService.LoginAsync(loginRequest);

            if (loginResponse != null && loginResponse.Errors == null)
                return Ok(loginResponse);
            else
                return BadRequest(loginResponse);
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

                var template = EmailTemplate.ConfirmEmail.Replace("[confirmUrl]", callbackUrl);

                //TODO: To set the URL of a front-end page which will then call the ConfirmEmail webApi and then redirect to the login
                await emailService.SendEmailAsync("MultiTenant - Email Confirm", template, new List<string>() { registerRequest.Email });
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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MultiTenantManagement.Abstractions.Models.Dto;
using MultiTenantManagement.Abstractions.Services;

namespace MultiTenantManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService identityService;

        public AuthController(IIdentityService identityService, ILogger<AuthController> logger)
        {
            this.identityService = identityService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginRequestDto loginRequest)
        {
            /*
             * Users:
             * admin@admin.com - Admin123! - Admin + User
             * 
             * {
                  "firstName": "Mattia",
                  "lastName": "Deidda",
                  "email": "m.deidda@tenant.it",
                  "password": "Tenant123!"
                }

                {
                  "firstName": "Mattia2",
                  "lastName": "Deidda2",
                  "email": "m.deidda2@tenant.it",
                  "password": "Tenant123!"
                }

                {
                  "firstName": "Samuel",
                  "lastName": "Deidda",
                  "email": "s.deidda@tenant.it",
                  "password": "Tenant123!"
                }

                {
                  "firstName": "Jessica",
                  "lastName": "malorgio",
                  "email": "j.malorgio@tenant.it",
                  "password": "Tenant123!"
                }
             */

            var loginResponse = await identityService.LoginAsync(loginRequest);

            if (loginResponse != null)
                return Ok(loginResponse);
            else
                return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequestDto refreshTokenRequest)
        {
            var loginResponse = await identityService.RefreshTokenAsync(refreshTokenRequest);

            if (loginResponse != null)
                return Ok(loginResponse);
            else
                return BadRequest();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequest)
        {
            var registerResponse = await identityService.RegisterAsync(registerRequest);

            return StatusCode(registerResponse.IsSuccess ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest,
                registerResponse);

        }
    }
}
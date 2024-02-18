using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MultiTenantManagement.Abstractions.Configurations.Options;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Login;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Register;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Tenant;
using MultiTenantManagement.Abstractions.Models.Dto.Authentication.Token;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Sql.DatabaseContext;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MultiTenantManagement.Dependencies.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly JwtOptions jwtOptions;
        private readonly AuthenticationDbContext authenticationDbContext;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly ITenantService tenantService;
        private readonly IMapper mapper;

        public IdentityService(
            IOptions<JwtOptions> jwtOptions,
            AuthenticationDbContext authenticationDbContext,
            UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, 
            ITenantService tenantService,
            IMapper mapper)
        {
            this.jwtOptions = jwtOptions.Value;
            this.authenticationDbContext = authenticationDbContext;
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.tenantService = tenantService;
            this.mapper = mapper;
        }

        public async Task<LoginResponseDto?> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var loginResponse = await signInManager.PasswordSignInAsync(loginRequestDto.Username, loginRequestDto.Password, false, true);

            if (!loginResponse.Succeeded)
            {
                var error = string.Empty;

                if (loginResponse.IsLockedOut)
                    error = "User Locked Out";
                else if (loginResponse.IsNotAllowed)
                    error = "User Not Allowed";

                return new LoginResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string>()
                    {
                       error
                    }
                };
            }

            var user = await userManager.FindByNameAsync(loginRequestDto.Username);
            var userRoles = await userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName!),
                new Claim(ClaimTypes.Surname, user.LastName ?? string.Empty),
                new Claim(ClaimTypes.GroupSid, user.TenantId?.ToString() ?? string.Empty)
            }
            .Union(userRoles.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToList();

            var token = GenerateToken(claims);

            //Save Refresh token properties to DB
            user.RefreshToken = token.RefreshToken;
            user.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtOptions.RefreshTokenExpirationMinutes);

            _ = await userManager.UpdateAsync(user);

            return new LoginResponseDto
            {
                IsSuccess = true,
                TokenDto = token
            };
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
        {
            var tokenValidation = await ValidateAccessToken(refreshTokenRequestDto.Token!);

            if (tokenValidation != null && tokenValidation.IsValid)
            {
                var userId = tokenValidation.ClaimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    var user = await userManager.FindByIdAsync(userId);

                    //Check token values to DB
                    if (user?.RefreshToken == null ||
                        user?.RefreshTokenExpirationDate < DateTime.UtcNow ||
                        user?.RefreshToken != refreshTokenRequestDto.RefreshToken)
                    {
                        return null;
                    }

                    var token = GenerateToken(tokenValidation.ClaimsIdentity.Claims);

                    user!.RefreshToken = token.RefreshToken;
                    user.RefreshTokenExpirationDate = DateTime.UtcNow.AddMinutes(jwtOptions.RefreshTokenExpirationMinutes);

                    _ = await userManager.UpdateAsync(user);

                    return new LoginResponseDto 
                    { 
                        IsSuccess = true,
                        TokenDto = token
                    };
                }
            }

            return null;
        }

        public async Task<RegisterResponseDto> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            using var transaction = authenticationDbContext.Database.BeginTransaction();
            TenantDto? tenantDto = null;

            try
            {
                var response = await tenantService.CreateTenantAsync(registerRequestDto.TenantId ?? Guid.Empty);

                if (response.IsSuccess)
                {
                    tenantDto = response.Tenant!;
                }
                else
                    return mapper.Map<RegisterResponseDto>(response);

                var user = mapper.Map<ApplicationUser>(registerRequestDto);
                user.TenantId = tenantDto.Id;

                var createdResult = await userManager.CreateAsync(user, registerRequestDto.Password);

                if (!createdResult.Succeeded)
                {
                    await transaction.RollbackAsync();
                    await tenantService.DeleteTenantAsync(tenantDto);

                    return mapper.Map<RegisterResponseDto>(createdResult);
                }

                _ = await userManager.AddToRoleAsync(user, CustomRoles.User); //Default Registration Role
                //_ = await userManager.AddClaimsAsync(user, claims); //Default Registration Claims

                await transaction.CommitAsync();

                var result = mapper.Map<RegisterResponseDto>(createdResult);
                result.User = user;

                return result;
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                await tenantService.DeleteTenantAsync(tenantDto);

                return new RegisterResponseDto
                {
                    IsSuccess = false,
                    Errors = new List<string>() { "An error occurred during transaction executing.", $"{ex.Message}" }
                };
            }
        }

        private async Task<TokenValidationResult> ValidateAccessToken(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtOptions.Audience,
                ValidateLifetime = false, // set false to allow access to the user without checking the token expiration
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Signature!)),
                RequireExpirationTime = true,
                ClockSkew = TimeSpan.FromMinutes(5)
            };

            var tokenValidation = await new JwtSecurityTokenHandler().ValidateTokenAsync(token, tokenValidationParameters);

            return tokenValidation;
        }

        private TokenDto GenerateToken(IEnumerable<Claim> claims)
        {
            var symmetricSignature = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Signature!));
            var signingCredentials = new SigningCredentials(symmetricSignature, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                jwtOptions.Issuer,
                jwtOptions.Audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(jwtOptions.AccessTokenExpirationMinutes),
                signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return new TokenDto
            {
                Token = token,
                RefreshToken = GenerateRefreshToken()
            };
        }

        private string GenerateRefreshToken()
        {
            return Utils.GenerateRamdomString(256);
        }
    }
}

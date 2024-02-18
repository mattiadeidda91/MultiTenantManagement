using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //TODO: To manage a generic API response

        private readonly UserManager<ApplicationUser> userManager;
        private readonly IAuthenticationDbContext authenticationDbContext;

        public UserController(UserManager<ApplicationUser> userManager, IAuthenticationDbContext authenticationDbContext)
        {
            this.userManager = userManager;
            this.authenticationDbContext = authenticationDbContext;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.Reader)]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var user = await authenticationDbContext.GetData<ApplicationUser>().OrderBy(u=> u.LastName).ToListAsync();

            return StatusCode(user != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, user);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.Reader)]
        [HttpGet("users-by-id")]
        public async Task<IActionResult> GetUserById([Required] Guid id)
        {
            var user = await userManager.FindByIdAsync(id.ToString());

            return StatusCode(user != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, user);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.Reader)]
        [HttpGet("user-by-username")]
        public async Task<IActionResult> GetUserByUsername([Required] string username)
        {
            var user = await userManager.FindByEmailAsync(username);

            return StatusCode(user != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, user);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.Reader)]
        [HttpGet("is-locked")]
        public async Task<IActionResult> IsLocked([Required] string username)
        {
            var user = await userManager.FindByEmailAsync(username);

            if (user != null)
            {
                var isEnable = await userManager.IsLockedOutAsync(user);  //user.LockoutEnd.GetValueOrDefault() <= DateTimeOffset.UtcNow

                return Ok(isEnable);
            }
            else
                return NotFound();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.Reader)]
        [HttpGet("roles")]
        public async Task<IActionResult> GetUserRoles([Required] string username)
        {
            var user = await userManager.FindByEmailAsync(username);
            if (user != null)
            {
                var claims = await userManager.GetRolesAsync(user);

                return Ok(claims);
            }
            else
                return NotFound();
        }

        [AuthRole(CustomRoles.Administrator)]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([Required] ApplicationUser user)
        {
            var result = await userManager.UpdateAsync(user);

            return StatusCode(result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest, user);
        }

        [AuthRole(CustomRoles.Administrator)]
        [HttpPut("set-lockout-enable")]
        public async Task<IActionResult> DisableUser([Required] string username, bool isEnable, int days = 7)
        {
            var user = await userManager.FindByEmailAsync(username);

            if (user != null)
            {
                var result = await userManager.SetLockoutEndDateAsync(user, isEnable ? null : DateTimeOffset.UtcNow.AddDays(days)); //block user for 7 days by default

                return StatusCode(result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest);
            }
            else
                return NotFound("User not found");
        }

        [AuthRole(CustomRoles.Administrator)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([Required] string username)
        {
            //TODO: To manage the logical deletion of the user or a control that if it is the only user associated with a specific tenant, then delete the associated tenant and the database
            var user = await userManager.FindByIdAsync(username);

            if (user != null)
            {
                var result = await userManager.DeleteAsync(user);

                return StatusCode(result.Succeeded ? StatusCodes.Status200OK : StatusCodes.Status400BadRequest);
            }
            else
                return NotFound();
        }
    }
}

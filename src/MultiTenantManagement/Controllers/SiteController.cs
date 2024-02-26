using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public SiteController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("sites")]
        public async Task<IActionResult> GetSites()
        {
            var sites = await applicationDbContext.GetData<Site>()
                .ToListAsync();

            var results = mapper.Map<IEnumerable<SiteDto>>(sites);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("site-by-id")]
        public async Task<IActionResult> GetSiteById([Required] Guid id)
        {
            var site = await applicationDbContext.GetData<Site>()
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<SiteDto>(site);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("site-by-name")]
        public async Task<IActionResult> GetSiteByName([Required] string name)
        {
            var site = await applicationDbContext.GetData<Site>()
                .FirstOrDefaultAsync(c => c.Name!.ToLowerInvariant() == name.ToLowerInvariant());

            var result = mapper.Map<SiteDto>(site);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> CreateSite([Required] SiteDto siteDto)
        {
            var site = mapper.Map<Site>(siteDto);

            if (site != null)
            {
                applicationDbContext.Insert(site);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateSite([Required] SiteDto siteDto)
        {
            var siteDb = await applicationDbContext.GetData<Site>()
                .FirstOrDefaultAsync(c => c.Id == siteDto.Id);

            if (siteDb == null)
                return NotFound();

            var site = mapper.Map<Site>(siteDto);

            if (site != null)
            {
                applicationDbContext.Update(site);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteSite([Required] Guid id)
        {
            var site = await applicationDbContext.GetData<Site>().FirstOrDefaultAsync(c => c.Id == id);

            if (site != null)
            {
                applicationDbContext.Delete(site);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

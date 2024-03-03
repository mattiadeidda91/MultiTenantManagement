using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Activity;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Customer;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Site;
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
        [HttpGet]
        public async Task<IActionResult> GetSites()
        {
            var sites = await applicationDbContext.GetData<Site>()
                .ToListAsync();

            var results = mapper.Map<IEnumerable<SiteDto>>(sites);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetSiteById([Required] Guid id)
        {
            var site = await applicationDbContext.GetData<Site>()
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<SiteDto>(site);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("{id}/customers")]
        public async Task<IActionResult> GetCustomers([Required] Guid id)
        {
            var customer = await applicationDbContext.GetData<Customer>()
                .Include(c => c.Certificates)
                .Include(c => c.FederalCards)
                .Include(c => c.MembershipCards)
                .Include(c => c.CustomersActivities)
                    .ThenInclude(ca => ca.Activity)
                .Where(c => c.SiteId == id)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<CustomerWithoutSiteDto>>(customer);

            return StatusCode(customer != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("{id}/activities")]
        public async Task<IActionResult> GetActivities([Required] Guid id)
        {
            var activity = await applicationDbContext.GetData<Activity>()
                .Include(a => a.Hours)
                .Include(a => a.Rates)
                .Include(a => a.CustomersActivities)
                    .ThenInclude(ca => ca.Customer)
                .Where(a => a.SiteId == id)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<ActivityWithoutSiteDto>>(activity);

            return StatusCode(activity != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-name")]
        public async Task<IActionResult> GetSiteByName([Required] string name)
        {
            var site = await applicationDbContext.GetData<Site>()
                .FirstOrDefaultAsync(c => c.Name!.ToLower() == name.ToLower());

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

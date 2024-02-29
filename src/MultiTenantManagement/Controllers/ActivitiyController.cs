﻿using AutoMapper;
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
    public class ActivitiyController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public ActivitiyController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("activities")]
        public async Task<IActionResult> GetActivities()
        {
            var activties = await applicationDbContext.GetData<Activity>()
                .Include(a => a.HoursActivities)
                .Include(a => a.Rates)
                .Include(a => a.CustomersActivities)!
                    .ThenInclude(ca => ca.Customer)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<ActivityDto>>(activties);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("activity-by-id")]
        public async Task<IActionResult> GetActivityById([Required] Guid id)
        {
            var activity = await applicationDbContext.GetData<Activity>()
                .Include(a => a.HoursActivities)
                .Include(a => a.Rates)
                .Include(a => a.CustomersActivities)
                    .ThenInclude(ca => ca.Customer)
                .FirstOrDefaultAsync(a => a.Id == id);

            var result = mapper.Map<ActivityDto>(activity);

            return StatusCode(activity != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("activity-by-site-id")]
        public async Task<IActionResult> GetActivityBySiteId([Required] Guid siteId)
        {
            var activity = await applicationDbContext.GetData<Activity>()
               .Include(a => a.HoursActivities)
               .Include(a => a.Rates)
               .Include(a => a.CustomersActivities)
                   .ThenInclude(ca => ca.Customer)
               .FirstOrDefaultAsync(a => a.SiteId == siteId);

            var result = mapper.Map<ActivityDto>(activity);

            return StatusCode(activity != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("activity-by-name")]
        public async Task<IActionResult> GetActivityByName([Required] string name)
        {
            var activity = await applicationDbContext.GetData<Activity>()
               .Include(a => a.HoursActivities)
                .Include(a => a.Rates)
                .Include(a => a.CustomersActivities)
                    .ThenInclude(ca => ca.Activity)
                .Where(a => a.Name!.ToLowerInvariant().Contains(name.ToLowerInvariant()))
                .ToListAsync();

            var result = mapper.Map<ActivityDto>(activity);

            return StatusCode(activity != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> CreateActivity([Required] RequestActivity request)
        {
            var activity = mapper.Map<Activity>(request);

            if (activity != null)
            {
                applicationDbContext.Insert(activity);
                await applicationDbContext.SaveAsync();

                if(activity.Id != Guid.Empty)
                {
                    var customer = await applicationDbContext.GetData<Customer>().FirstOrDefaultAsync(c => c.Id == request.CustomerId);

                    if(customer != null)
                    {
                        applicationDbContext.Insert(new CustomerActivity() { ActivityId = activity.Id, CustomerId = customer.Id });
                        await applicationDbContext.SaveAsync();
                    }
                }

                return Ok();
            }
            else
                return NotFound();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteActivity([Required] Guid id)
        {
            //TODO: To manage the logical deletion of the activity or a control that if it is the only user associated with a specific tenant, then delete the associated tenant and the database
            var activity = await applicationDbContext.GetData<Activity>().FirstOrDefaultAsync(c => c.Id == id);

            if (activity != null)
            {
                applicationDbContext.Delete(activity);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}
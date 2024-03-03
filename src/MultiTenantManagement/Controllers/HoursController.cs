using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Hours;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Hours.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoursController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public HoursController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GeHours()
        {
            var hours = await applicationDbContext.GetData<Hours>()
                .Include(c => c.Activity)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<HoursDto>>(hours);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetHourById([Required] Guid id)
        {
            var hour = await applicationDbContext.GetData<Hours>()
                .Include(c => c.Activity)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<HoursDto>(hour);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-activity-id")]
        public async Task<IActionResult> GetHoursByActivityId([Required] Guid activityId)
        {
            var hours = await applicationDbContext.GetData<Hours>()
                .Include(c => c.Activity)
                .Where(c => c.Activity.Id == activityId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<HoursDto>>(hours);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-day")]
        public async Task<IActionResult> GetHourByName([Required] DayOfWeek day)
        {
            var hours = await applicationDbContext.GetData<Hours>()
                .Include(c => c.Activity)
                .Where(r => r.Day!.ToLower() == day.GetDisplayName().ToLower())
                .ToListAsync();

            var result = mapper.Map<IEnumerable<HoursDto>>(hours);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddHour([Required] RequestHour request)
        {
            var hour = mapper.Map<Hours>(request);

            if (hour != null)
            {
                applicationDbContext.Insert(hour);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateHour([Required] RequestHour request)
        {
            var hourDb = await applicationDbContext.GetData<Hours>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (hourDb == null)
                return NotFound();

            var hour = mapper.Map<Hours>(request);

            if (hour != null)
            {
                applicationDbContext.Update(hour);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteHour([Required] Guid id)
        {
            var hour = await applicationDbContext.GetData<Hours>().FirstOrDefaultAsync(c => c.Id == id);

            if (hour != null)
            {
                applicationDbContext.Delete(hour);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

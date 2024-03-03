using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Rates;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Rates.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public RatesController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GetRates()
        {
            var rates = await applicationDbContext.GetData<Rates>()
                .Include(c => c.Activity)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<RatesDto>>(rates);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetRateById([Required] Guid id)
        {
            var rate = await applicationDbContext.GetData<Rates>()
                .Include(c => c.Activity)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<RatesDto>(rate);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-activity-id")]
        public async Task<IActionResult> GetRatesByActivityId([Required] Guid activityId)
        {
            var rates = await applicationDbContext.GetData<Rates>()
                .Include(c => c.Activity)
                .Where(c => c.Activity.Id == activityId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<RatesDto>>(rates);

            return StatusCode(rates != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-day")]
        public async Task<IActionResult> GetRateByName([Required] DayOfWeek day)
        {
            var rates = await applicationDbContext.GetData<Rates>()
                .Include(c => c.Activity)
                .Where(r => r.DayOfWeek != null && r.DayOfWeek.Contains(day.GetDisplayName()))
                .ToListAsync();

            var result = mapper.Map<IEnumerable<RatesDto>>(rates);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddRate([Required] RequestRate request)
        {
            var rate = mapper.Map<Rates>(request);

            if (rate != null)
            {
                applicationDbContext.Insert(rate);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateRate([Required] RequestRate request)
        {
            var rateDb = await applicationDbContext.GetData<Rates>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (rateDb == null)
                return NotFound();

            var rate = mapper.Map<Rates>(request);

            if (rate != null)
            {
                applicationDbContext.Update(rate);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteRate([Required] Guid id)
        {
            var rate = await applicationDbContext.GetData<Rates>().FirstOrDefaultAsync(c => c.Id == id);

            if (rate != null)
            {
                applicationDbContext.Delete(rate);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

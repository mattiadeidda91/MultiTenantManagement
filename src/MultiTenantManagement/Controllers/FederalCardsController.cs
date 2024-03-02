using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FederalCardsController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public FederalCardsController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GetFederalCards()
        {
            var federalCards = await applicationDbContext.GetData<FederalCard>()
                .Include(c => c.Customer)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<FederalCardDto>>(federalCards);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetFederalCardById([Required] Guid id)
        {
            var federalCard = await applicationDbContext.GetData<FederalCardDto>()
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<FederalCardDto>(federalCard);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-customer-id")]
        public async Task<IActionResult> GetFederalCardsByCustomerId([Required] Guid customerId)
        {
            var federalCards = await applicationDbContext.GetData<FederalCard>()
                .Include(c => c.Customer)
                .Where(c => c.Customer.Id == customerId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<FederalCardDto>>(federalCards);

            return StatusCode(federalCards != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-number")]
        public async Task<IActionResult> GetFederalCardByName([Required] string cardNumber)
        {
            var federalCard = await applicationDbContext.GetData<FederalCard>()
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Card.ToLower() == cardNumber);

            var result = mapper.Map<FederalCardDto>(federalCard);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddFederalCard([Required] RequestFederalCard request)
        {
            var federalCard = mapper.Map<FederalCard>(request);

            if (federalCard != null)
            {
                applicationDbContext.Insert(federalCard);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateFederalCard([Required] RequestFederalCard request)
        {
            var federalCardDb = await applicationDbContext.GetData<FederalCard>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (federalCardDb == null)
                return NotFound();

            var federalCard = mapper.Map<FederalCard>(request);

            if (federalCard != null)
            {
                applicationDbContext.Update(federalCard);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteFederalCard([Required] Guid id)
        {
            var federalCard = await applicationDbContext.GetData<FederalCard>().FirstOrDefaultAsync(c => c.Id == id);

            if (federalCard != null)
            {
                applicationDbContext.Delete(federalCard);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

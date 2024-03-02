using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.FederalCard.Request;
using MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard;
using MultiTenantManagement.Abstractions.Models.Dto.Application.MembershipCard.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembershipCardsController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public MembershipCardsController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GetMembershipCards()
        {
            var membershipCards = await applicationDbContext.GetData<MembershipCard>()
                .Include(c => c.Customer)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<MembershipCardDto>>(membershipCards);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetMembershipCardById([Required] Guid id)
        {
            var membershipCard = await applicationDbContext.GetData<MembershipCard>()
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<MembershipCardDto>(membershipCard);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-customer-id")]
        public async Task<IActionResult> GetMembershipCardsByCustomerId([Required] Guid customerId)
        {
            var membershipCards = await applicationDbContext.GetData<MembershipCard>()
                .Include(c => c.Customer)
                .Where(c => c.Customer.Id == customerId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<MembershipCardDto>>(membershipCards);

            return StatusCode(membershipCards != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-number")]
        public async Task<IActionResult> GetMembershipCardByName([Required] string cardNumber)
        {
            var membershipCard = await applicationDbContext.GetData<MembershipCard>()
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Card.ToLower() == cardNumber);

            var result = mapper.Map<MembershipCardDto>(membershipCard);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddMembershipCard([Required] RequestMembershipCard request)
        {
            var membershipCard = mapper.Map<MembershipCard>(request);

            if (membershipCard != null)
            {
                applicationDbContext.Insert(membershipCard);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateMembershipCard([Required] RequestFederalCard request)
        {
            var membershipCardDb = await applicationDbContext.GetData<MembershipCard>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (membershipCardDb == null)
                return NotFound();

            var membershipCard = mapper.Map<MembershipCard>(request);

            if (membershipCard != null)
            {
                applicationDbContext.Update(membershipCard);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteMembershipCard([Required] Guid id)
        {
            var membershipCard = await applicationDbContext.GetData<MembershipCard>().FirstOrDefaultAsync(c => c.Id == id);

            if (membershipCard != null)
            {
                applicationDbContext.Delete(membershipCard);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

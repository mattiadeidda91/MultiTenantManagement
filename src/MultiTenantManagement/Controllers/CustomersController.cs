﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Models.Entities.Authentication;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public CustomersController(UserManager<ApplicationUser> userManager, IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("customers")]
        public async Task<IActionResult> GetCustomers()
        {
            var customers = await applicationDbContext.GetData<Customer>()
                .Include(c => c.Site)
                .Include(c => c.Certificates)
                .Include(c => c.FederalCards)
                .Include(c => c.MembershipCards)
                .Include(c => c.CustomersActivities)
                    .ThenInclude(ca => ca.Activity)        
                .ToListAsync();

            var results = mapper.Map<IEnumerable<CustomerDto>>(customers);

            return StatusCode(customers != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("customer-by-id")]
        public async Task<IActionResult> GetUserById([Required] Guid id)
        {
            var customer = await applicationDbContext.GetData<Customer>()
                .Include(c => c.Site)
                .Include(c => c.Certificates)
                .Include(c => c.FederalCards)
                .Include(c => c.MembershipCards)
                .Include(c => c.CustomersActivities)
                    .ThenInclude(ca => ca.Activity)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<CustomerDto>(customer);

            return StatusCode(customer != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> CreateUser([Required] CustomerDto customerDto)
        {
            var user = mapper.Map<Customer>(customerDto);

            if (user != null)
            {
                applicationDbContext.Insert(user);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteUser([Required] Guid id)
        {
            //TODO: To manage the logical deletion of the user or a control that if it is the only user associated with a specific tenant, then delete the associated tenant and the database
            var user = await applicationDbContext.GetData<Customer>().FirstOrDefaultAsync(c => c.Id == id);

            if (user != null)
            {
                applicationDbContext.Delete(user);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Invoice;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Invoice.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public InvoicesController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GetInvoices()
        {
            var invoices = await applicationDbContext.GetData<Invoice>()
                .Include(i => i.Site)
                .Include(i => i.Customer)
                .Include(i => i.Rate)
                    .ThenInclude(i => i.Activity)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<InvoiceDto>>(invoices);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetInvoiceById([Required] Guid id)
        {
            var invoice = await applicationDbContext.GetData<Invoice>()
                .Include(i => i.Site)
                .Include(i => i.Customer)
                .Include(i => i.Rate)
                    .ThenInclude(i => i.Activity)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<InvoiceDto>(invoice);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-number")]
        public async Task<IActionResult> GetInvoiceByNumber([Required] string invoiceNumber)
        {
            var invoice = await applicationDbContext.GetData<Invoice>()
                .Include(i => i.Site)
                .Include(i => i.Customer)
                .Include(i => i.Rate)
                    .ThenInclude(i => i.Activity)
                .FirstOrDefaultAsync(c => c.InvoiceNumber.ToLower() == invoiceNumber.ToLower());

            var result = mapper.Map<InvoiceDto>(invoice);

            return StatusCode(invoice != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddInvoice([Required] RequestInvoice request)
        {
            var invoice = mapper.Map<Invoice>(request);

            if (invoice != null)
            {
                applicationDbContext.Insert(invoice);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateInvoice([Required] RequestInvoice request)
        {
            var invoiceDb = await applicationDbContext.GetData<Invoice>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (invoiceDb == null)
                return NotFound();

            var invoice = mapper.Map<Invoice>(request);

            if (invoice != null)
            {
                applicationDbContext.Update(invoice);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteInvoice([Required] Guid id)
        {
            var invoice = await applicationDbContext.GetData<Invoice>().FirstOrDefaultAsync(c => c.Id == id);

            if (invoice != null)
            {
                applicationDbContext.Delete(invoice);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

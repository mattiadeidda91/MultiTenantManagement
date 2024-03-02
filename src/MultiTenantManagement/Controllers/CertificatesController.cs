using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Certificate.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificatesController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public CertificatesController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GetCertificates()
        {
            var certificates = await applicationDbContext.GetData<Certificate>()
                .Include(c => c.Customer)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<CertificateDto>>(certificates);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetCertificateById([Required] Guid id)
        {
            var certificate = await applicationDbContext.GetData<Certificate>()
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<CertificateDto>(certificate);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-customer-id")]
        public async Task<IActionResult> GetCertificatesByCustomerId([Required] Guid customerId)
        {
            var certificates = await applicationDbContext.GetData<Certificate>()
                .Include(c => c.Customer)
                .Where(c => c.Customer.Id == customerId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<CertificateDto>>(certificates);

            return StatusCode(certificates != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-number")]
        public async Task<IActionResult> GetSiteByName([Required] string certificateNumber)
        {
            var certificate = await applicationDbContext.GetData<Certificate>()
                .Include(c => c.Customer)
                .FirstOrDefaultAsync(c => c.CertificateNumber!.ToLower() == certificateNumber);

            var result = mapper.Map<CertificateDto>(certificate);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddCertificate([Required] RequestCertificate request)
        {
            var certificate = mapper.Map<Certificate>(request);

            if (certificate != null)
            {
                applicationDbContext.Insert(certificate);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateCertificate([Required] RequestCertificate request)
        {
            var certificateDb = await applicationDbContext.GetData<Site>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (certificateDb == null)
                return NotFound();

            var certificate = mapper.Map<Certificate>(request);

            if (certificate != null)
            {
                applicationDbContext.Update(certificate);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteCertificate([Required] Guid id)
        {
            var certificate = await applicationDbContext.GetData<Certificate>().FirstOrDefaultAsync(c => c.Id == id);

            if (certificate != null)
            {
                applicationDbContext.Delete(certificate);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Expense;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Expense.Request;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Abstractions.Utilities.Costants;
using MultiTenantManagement.Attributes;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly IApplicationDbContext applicationDbContext;
        private readonly IMapper mapper;

        public ExpensesController(IApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this.applicationDbContext = applicationDbContext;
            this.mapper = mapper;
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet]
        public async Task<IActionResult> GeExpenses()
        {
            var expenses = await applicationDbContext.GetData<Expense>()
                .Include(c => c.Site)
                .ToListAsync();

            var results = mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return StatusCode(results != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, results);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-id")]
        public async Task<IActionResult> GetExpenseById([Required] Guid id)
        {
            var expense = await applicationDbContext.GetData<Expense>()
                .Include(c => c.Site)
                .FirstOrDefaultAsync(c => c.Id == id);

            var result = mapper.Map<ExpenseDto>(expense);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-activity-id")]
        public async Task<IActionResult> GetExpensesBySiteId([Required] Guid siteId)
        {
            var expenses = await applicationDbContext.GetData<Expense>()
                .Include(c => c.Site)
                .Where(c => c.Site.Id == siteId)
                .ToListAsync();

            var result = mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-activity")]
        public async Task<IActionResult> GetExpensesByActivityName([Required] string activityName)
        {
            var expenses = await applicationDbContext.GetData<Expense>()
                .Include(c => c.Site)
                .Where(r => r.ActivityName != null && r.ActivityName.ToLower() == activityName.ToLower())
                .ToListAsync();

            var result = mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User, CustomRoles.Reader)]
        [HttpGet("by-description")]
        public async Task<IActionResult> GetExpensesByDescription([Required] string description)
        {
            var expenses = await applicationDbContext.GetData<Expense>()
                .Include(c => c.Site)
                .Where(r => r.Description != null && r.Description.ToLower().Contains(description.ToLower()))
                .ToListAsync();

            var result = mapper.Map<IEnumerable<ExpenseDto>>(expenses);

            return StatusCode(result != null ? StatusCodes.Status200OK : StatusCodes.Status404NotFound, result);
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPost]
        public async Task<IActionResult> AddExpense([Required] RequestExpense request)
        {
            var expense = mapper.Map<Expense>(request);

            if (expense != null)
            {
                applicationDbContext.Insert(expense);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }

        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpPut]
        public async Task<IActionResult> UpdateExpense([Required] RequestExpense request)
        {
            var expenseDb = await applicationDbContext.GetData<Expense>()
                .FirstOrDefaultAsync(c => c.Id == request.Id);

            if (expenseDb == null)
                return NotFound();

            var expense = mapper.Map<Expense>(request);

            if (expense != null)
            {
                applicationDbContext.Update(expense);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return BadRequest();
        }


        [AuthRole(CustomRoles.Administrator, CustomRoles.User)]
        [HttpDelete]
        public async Task<IActionResult> DeleteExpense([Required] Guid id)
        {
            var expense = await applicationDbContext.GetData<Expense>().FirstOrDefaultAsync(c => c.Id == id);

            if (expense != null)
            {
                applicationDbContext.Delete(expense);
                await applicationDbContext.SaveAsync();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}

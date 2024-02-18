using Microsoft.AspNetCore.Mvc;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Product;
using MultiTenantManagement.Abstractions.Services;

namespace MultiTenantManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var products = await productService.GetAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductDto product)
        {
            await productService.SaveAsync(product);
            return NoContent();
        }
    }
}

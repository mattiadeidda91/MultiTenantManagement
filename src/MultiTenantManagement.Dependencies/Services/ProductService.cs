using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto;
using MultiTenantManagement.Abstractions.Models.Entities;
using MultiTenantManagement.Abstractions.Services;
using MultiTenantManagement.Sql.DatabaseContext;

namespace MultiTenantManagement.Dependencies.Services
{
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext dataContext;
        private readonly IMapper mapper;

        public ProductService(ApplicationDbContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAsync()
        {
            var products = await dataContext.Products.OrderBy(p => p.Name).ToListAsync();

            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);

            return productsDto;
        }

        public async Task SaveAsync(ProductDto product)
        {
            var dbProduct = mapper.Map<Product>(product);

            dataContext.Products.Add(dbProduct);
            await dataContext.SaveChangesAsync();
        }
    }
}

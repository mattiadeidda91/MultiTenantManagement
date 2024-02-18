using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MultiTenantManagement.Abstractions.Models.Dto.Application.Product;
using MultiTenantManagement.Abstractions.Models.Entities.Application;
using MultiTenantManagement.Abstractions.Services;

namespace MultiTenantManagement.Dependencies.Services
{
    public class ProductService : IProductService
    {
        private readonly IApplicationDbContext dataContext;
        private readonly IMapper mapper;

        public ProductService(IApplicationDbContext dataContext, IMapper mapper)
        {
            this.dataContext = dataContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAsync()
        {
            var products = await dataContext.GetData<Product>().OrderBy(p => p.Name).ToListAsync();

            var productsDto = mapper.Map<IEnumerable<ProductDto>>(products);

            return productsDto;
        }

        public async Task SaveAsync(ProductDto product)
        {
            var dbProduct = mapper.Map<Product>(product);
            
            dataContext.Insert(dbProduct);
            await dataContext.SaveAsync();
        }
    }
}

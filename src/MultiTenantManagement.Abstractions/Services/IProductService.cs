using MultiTenantManagement.Abstractions.Models.Dto.Application.Product;

namespace MultiTenantManagement.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAsync();
        Task SaveAsync(ProductDto product);
    }
}

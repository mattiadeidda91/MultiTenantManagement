using MultiTenantManagement.Abstractions.Models.Dto;

namespace MultiTenantManagement.Abstractions.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAsync();
        Task SaveAsync(ProductDto product);
    }
}

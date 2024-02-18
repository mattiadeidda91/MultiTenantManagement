using MultiTenantManagement.Abstractions.Models.Entities.Common;
using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Abstractions.Models.Entities.Application
{
    public class Product : TenantEntity
    {
        [Required]
        [MaxLength(50)]
        public string? Name { get; set; }

        public double Price { get; set; }
    }
}

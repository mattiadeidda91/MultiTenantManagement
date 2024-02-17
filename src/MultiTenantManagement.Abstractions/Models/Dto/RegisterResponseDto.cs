using MultiTenantManagement.Abstractions.Models.Entities;

namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class RegisterResponseDto
    {
        public ApplicationUser? User { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}

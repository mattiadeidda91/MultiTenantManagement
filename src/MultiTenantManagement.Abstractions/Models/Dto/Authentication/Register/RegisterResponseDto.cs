using MultiTenantManagement.Abstractions.Models.Entities.Authentication;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Register
{
    public class RegisterResponseDto
    {
        public ApplicationUser? User { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string>? Errors { get; set; }
    }
}

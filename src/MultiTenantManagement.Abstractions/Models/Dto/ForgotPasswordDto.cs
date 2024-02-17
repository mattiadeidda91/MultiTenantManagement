using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class ForgotPasswordDto
    {
        [Required]
        public string? Email { get; set; }
    }
}

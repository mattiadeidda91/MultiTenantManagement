using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.ForgotPassword
{
    public class ForgotPasswordDto
    {
        [Required]
        public string? Email { get; set; }
    }
}

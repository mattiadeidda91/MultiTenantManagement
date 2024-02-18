using System.ComponentModel.DataAnnotations;

namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.ResetPassword
{
    public class ResetPasswordDto
    {
        [Required]
        public string? UserId { get; set; }

        [Required]
        public string? Token { get; set; }

        [Required]
        public string? Password { get; set; }
    }
}

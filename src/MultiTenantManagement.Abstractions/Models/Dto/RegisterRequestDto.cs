using System.ComponentModel.DataAnnotations;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class RegisterRequestDto
    {
        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "The Username field must contain only alphanumeric characters.")]
        //public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        [Required]
        public string Password { get; set; }

        public Guid? TenantId { get; set; }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
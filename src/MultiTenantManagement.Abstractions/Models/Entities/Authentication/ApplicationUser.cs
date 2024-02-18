using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace MultiTenantManagement.Abstractions.Models.Entities.Authentication
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        //[RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "The Username field must contain only alphanumeric characters.")]
        //public override string UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpirationDate { get; set; }
        public Guid? TenantId { get; set; } //TBD: check if use Tenant class

        public virtual ICollection<ApplicationUserRole>? UserRoles { get; set; }
    }
}

#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

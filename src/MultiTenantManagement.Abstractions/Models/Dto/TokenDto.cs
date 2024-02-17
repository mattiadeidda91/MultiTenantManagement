namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class TokenDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

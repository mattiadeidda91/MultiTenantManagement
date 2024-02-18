namespace MultiTenantManagement.Abstractions.Models.Dto.Authentication.Token
{
    public class TokenDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

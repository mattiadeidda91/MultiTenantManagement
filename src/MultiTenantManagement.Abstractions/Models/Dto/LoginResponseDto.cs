namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class LoginResponseDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
    }
}

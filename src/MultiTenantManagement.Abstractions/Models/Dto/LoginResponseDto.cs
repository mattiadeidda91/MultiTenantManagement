namespace MultiTenantManagement.Abstractions.Models.Dto
{
    public class LoginResponseDto
    {
        public TokenDto? TokenDto { get; set; }
        public IEnumerable<string>? Errors { get; set; }
        public bool IsSuccess { get; set; }
    }
}

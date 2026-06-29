namespace WebApplication1.Service.DTOs
{
    public class AuthResponseDTO
    {
        public AuthUserDTO User { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public int ExpiresIn { get; set; } = 900; 
    }
}

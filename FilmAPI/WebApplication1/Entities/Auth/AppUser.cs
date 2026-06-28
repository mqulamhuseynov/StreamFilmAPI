using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Entities.Auth
{
    public class AppUser : IdentityUser<int>
    {
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();
        public ICollection<PasswordResetToken> PasswordResetTokens { get; set; }
            = new List<PasswordResetToken>();
    }
}

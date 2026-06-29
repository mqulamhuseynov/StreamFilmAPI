using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Entities.Auth;
using WebApplication1.Entities.Commons;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _user;
        private readonly AppDbContext _context;

        private const int AccessTokenMinutes = 15;  
        private const int RefreshTokenDays = 7;

        public AuthService(UserManager<AppUser> user, AppDbContext context)
        {
            _user = user;
            _context = context;
        }

        public async Task<ApiResponse<AuthResponseDTO>> Login(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email)) return ApiResponse<AuthResponseDTO>.FailResponse("email cant be null", 400);

            if (string.IsNullOrWhiteSpace(dto.Password)) return ApiResponse<AuthResponseDTO>.FailResponse("pass cant be null", 400);

            var user = await _user.FindByEmailAsync(dto.Email);
            if (user is null) return ApiResponse<AuthResponseDTO>.FailResponse("invalid email or pass", 401);

            if (!await _user.CheckPasswordAsync(user, dto.Password)) return ApiResponse<AuthResponseDTO>.FailResponse("email or pass invalid", 401);

            if (!user.IsActive) return ApiResponse<AuthResponseDTO>.FailResponse("user is not active", 403);

            var oldTokens = _context.RefreshTokens.Where(r => r.UserId == user.Id);
            _context.RefreshTokens.RemoveRange(oldTokens);

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshTokens();

            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenDays),
                CreatedAt = DateTime.UtcNow
            });
            await _context.SaveChangesAsync();

            return ApiResponse<AuthResponseDTO>.SuccessResponse(
                BuildResponse(user, accessToken, refreshToken),
                "Login successful");
        }

        public async Task<ApiResponse<AuthResponseDTO>> Register(RegisterDTO dto)
        {
            //yoxlamalar
            if (string.IsNullOrWhiteSpace(dto.Username) || dto.Username.Length < 3 || dto.Username.Length > 20)
                return ApiResponse<AuthResponseDTO>.FailResponse("username cant be less than 3, more than 20 and null", 400);
            if (string.IsNullOrWhiteSpace(dto.Email) || !IsValidEmail(dto.Email))
                return ApiResponse<AuthResponseDTO>.FailResponse("enter a valid mail", 400);
            if (string.IsNullOrWhiteSpace(dto.Password) || dto.Password.Length < 8)
                return ApiResponse<AuthResponseDTO>.FailResponse("password must be longer than 8 characters", 400);

            if (await _user.FindByEmailAsync(dto.Email) is not null)
                return ApiResponse<AuthResponseDTO>.FailResponse("email uje vare qaqa", 409);
            if (await _user.FindByNameAsync(dto.Username) is not null)
                return ApiResponse<AuthResponseDTO>.FailResponse("username already exists", 409);
            //user dto ve yaratmaq

            var user = new AppUser 
            {
             UserName = dto.Username,
             Email = dto.Email,
             IsActive = true,
             CreatedAt = DateTime.UtcNow,
             UpdatedAt = DateTime.UtcNow,
            };

          var result = await _user.CreateAsync(user, dto.Password);

            if (!result.Succeeded) return ApiResponse<AuthResponseDTO>.FailResponse(result.Errors.First().Description, 400);

            //tokenler
            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshTokens();

            await _context.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiresAt = DateTime.UtcNow.AddDays(RefreshTokenDays),
                CreatedAt = DateTime.UtcNow
            });

            await _context.SaveChangesAsync();

            return ApiResponse<AuthResponseDTO>.SuccessResponse(BuildResponse(user, accessToken, refreshToken));
        }


        //privates
        private string GenerateAccessToken(AppUser user)
        {
            var secret = Environment.GetEnvironmentVariable("jwtSecret")!;
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim("userId",   user.Id.ToString()),
            new Claim("email",    user.Email!),
            new Claim("username", user.UserName!)
        };

            var token = new JwtSecurityToken(
                expires: DateTime.UtcNow.AddMinutes(AccessTokenMinutes),
                claims: claims,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshTokens() 
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToHexString(bytes).ToLower();
        }

        private bool IsValidEmail(string email) => new EmailAddressAttribute().IsValid(email);

        private AuthResponseDTO BuildResponse(AppUser user, string access, string refresh)
        => new()
        {
            User = new AuthUserDTO
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                CreatedAt = user.CreatedAt
            },
            AccessToken = access,
            RefreshToken = refresh,
            ExpiresIn = 900
        };

    }
}

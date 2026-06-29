using WebApplication1.Entities.Commons;
using WebApplication1.Service.DTOs;

namespace WebApplication1.Service.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDTO>> Register(RegisterDTO dto);
        Task<ApiResponse<AuthResponseDTO>> Login(LoginDTO dto);
    }
}

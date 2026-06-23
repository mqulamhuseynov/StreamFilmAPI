using WebApplication1.Entities;
using WebApplication1.Entities.Commons;
using WebApplication1.Service.DTOs;

namespace WebApplication1.Service.Interfaces
{
    public interface IGenreService
    {
        public Task<ApiResponse<IEnumerable<GenreDTO>>> GetGenres(string? type);
    }
}

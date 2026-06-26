using WebApplication1.Entities.Commons;
using WebApplication1.Service.DTOs;

namespace WebApplication1.Service.Interfaces
{
    public interface IContentService
    {
        public Task<ApiResponse<IEnumerable<HeroDTO>>> GetHero();
        public Task<ApiResponse<IEnumerable<TopTenDTO>>> GetTopTen(string? type);
        public Task<ApiResponse<IEnumerable<ContentPageGenreDTO>>> GetContentPageGenre(string? type);
        public Task<ApiResponse<IEnumerable<ContentListDTO>>> GetContentList(string? type, string? filter, int limit);
    }
}

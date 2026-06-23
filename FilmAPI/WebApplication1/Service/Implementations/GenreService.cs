using WebApplication1.Entities;
using WebApplication1.Entities.Commons;
using WebApplication1.Enums;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository) => _genreRepository = genreRepository;

        public async Task<ApiResponse<IEnumerable<GenreDTO>>> GetGenres(string? type)
        {
            GenreType? parsedType = null;

            if (!string.IsNullOrWhiteSpace(type) && type.Equals("all", StringComparison.OrdinalIgnoreCase)) 
            {
                if(!Enum.TryParse<GenreType>(type,true,out var result))
                {
                    return ApiResponse<IEnumerable<GenreDTO>>.FailResponse("Failed auyes");
                }


                parsedType = result;
            }

            var genres = await _genreRepository.GetGenres(parsedType);

            var dto = genres.Select(m => new GenreDTO
            {
                Id = m.Id,
                Name = m.Name,
                Slug = m.Slug,
                Type = m.Type.ToString().ToLower(),
                CoverImage1 = m.CoverImage1,
                CoverImage2 = m.CoverImage2,
                CoverImage3 = m.CoverImage3,
                CoverImage4 = m.CoverImage4
            }).ToList();

            return ApiResponse<IEnumerable<GenreDTO>>.SuccessResponse(dto);
        }
    }
}

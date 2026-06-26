using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Identity.Client;
using System.Formats.Asn1;
using WebApplication1.Entities.Commons;
using WebApplication1.Enums;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations
{
    public class ContentService : IContentService
    {
        private readonly IContentRepository _contentRepository;
        private readonly IGenreRepository _genreRepository;

        public ContentService(IContentRepository contentRepository, IGenreRepository genreRepository)
        {
            _contentRepository = contentRepository;
            _genreRepository = genreRepository;
        }

        public async Task<ApiResponse<IEnumerable<ContentListDTO>>> GetContentList(string? type, string? filter, int limit)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(filter)) return ApiResponse<IEnumerable<ContentListDTO>>.FailResponse("type and filter required");
            if(!Enum.TryParse<ContentType>(type,true,out var contentType)) return ApiResponse<IEnumerable<ContentListDTO>>.FailResponse("wrong type");

            var validFilters = new[] { "trending", "new-release", "must-watch" };
            if (!validFilters.Contains(filter.ToLower())) return ApiResponse<IEnumerable<ContentListDTO>>.FailResponse("wrong filter");

            var contents = await _contentRepository.GetContents(contentType, limit, filter);

            var dto = contents.Select(c => new ContentListDTO
            {
                Id = c.Id,
                Title = c.Title,
                PosterUrl = c.PosterUrl,
                ImdbRating = c.ImdbRating,
                StreamvibeRating = c.StreamvibeRating,
                ReleaseYear = c.ReleaseYear,
                Type = c.Type.ToString().ToLower()
            });

            return ApiResponse<IEnumerable<ContentListDTO>>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<IEnumerable<ContentPageGenreDTO>>> GetContentPageGenre(string? type)
        {
            if (string.IsNullOrWhiteSpace(type)) return ApiResponse<IEnumerable<ContentPageGenreDTO>>.FailResponse("empty error");

            
            if(!Enum.TryParse<ContentType>(type,true,out var contentType)) return ApiResponse<IEnumerable<ContentPageGenreDTO>>.FailResponse("wrong type, available types: movie, show");

            var genreType = contentType == ContentType.Movie ? GenreType.Movie : GenreType.Show;
            var genres = await _genreRepository.GetGenres(genreType);

            var dto = genres.Select(g => new ContentPageGenreDTO
            {
                Id = g.Id,
                Name = g.Name,
                Slug = g.Slug,
                CoverImage1 = g.CoverImage1,
                CoverImage2 = g.CoverImage2,
                CoverImage3 = g.CoverImage3,
                CoverImage4 = g.CoverImage4
            });

            return ApiResponse<IEnumerable<ContentPageGenreDTO>>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<IEnumerable<HeroDTO>>> GetHero()
        {
            var data = await _contentRepository.GetFeatured();

            var dto = data.Select(c => new HeroDTO 
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                BackgroundUrl = c.BackgroundUrl,
                TrailerUrl = c.TrailerUrl,
                Type = c.Type.ToString().ToLower()
            });

            return ApiResponse < IEnumerable < HeroDTO >>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<IEnumerable<TopTenDTO>>> GetTopTen(string? type)
        {
            if (string.IsNullOrWhiteSpace(type)) 
            {
                return ApiResponse<IEnumerable<TopTenDTO>>.FailResponse("empty error");
            }
            if (!Enum.TryParse<ContentType>(type, true, out var mediaType)) 
            {
                return ApiResponse<IEnumerable<TopTenDTO>>.FailResponse("wrong type, available types: movie, show");
            }

            var contents = await _contentRepository.GetTopTen(mediaType);

            var dto = contents.Select(c => new TopTenDTO
            {
                Id = c.Id,
                Title = c.Title,
                PosterUrl = c.PosterUrl,
                TopTenRank = c.TopTenRank!.Value,
                Genres = c.ContentGenres.Select(cg => new TopTenGenreDTO
                {
                    Id = cg.Genre.Id,
                    Name = cg.Genre.Name
                })
            });
            return ApiResponse<IEnumerable<TopTenDTO>>.SuccessResponse(dto);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Identity.Client;
using System.Formats.Asn1;
using WebApplication1.Entities;
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

        public async Task<ApiResponse<ContentDetailDTO>> GetContentDetail(string? id)
        {
            if (!int.TryParse(id, out int result) || result <= 0) return ApiResponse<ContentDetailDTO>.FailResponse("type must be an int", 400);

            var content = await _contentRepository.GetContentDetailById(result);

            if (content == null) return ApiResponse<ContentDetailDTO>.FailResponse("content not found XDDDDDDDD", 404);


            var dto = new ContentDetailDTO
            {
                Id = content.Id,
                Title = content.Title,
                Description = content.Description,
                PosterUrl = content.PosterUrl,
                BackgroundUrl = content.BackgroundUrl,
                TrailerUrl = content.TrailerUrl,
                Type = content.Type.ToString().ToLower(),
                ReleaseYear = content.ReleaseYear,
                ImdbRating = content.ImdbRating,
                StreamvibeRating = content.StreamvibeRating,

                Genres = content.ContentGenres.Select(cg => new ContentDetailGenreDTO
                {
                    Id = cg.Genre.Id,
                    Name = cg.Genre.Name,
                    Slug = cg.Genre.Slug
                }),

                Languages = content.ContentLanguages.Select(cl => cl.Language),

                Cast = content.ContentPeople
        .Where(cp => cp.RoleType == RoleType.Actor)
        .Select(cp => new CastMemberDTO
        {
            Id = cp.Person.Id,
            Name = cp.Person.Name,
            AvatarUrl = cp.Person.AvatarUrl,
            Nationality = cp.Person.Nationality,
            CharacterName = cp.CharacterName
        }),

                Directors = content.ContentPeople
        .Where(cp => cp.RoleType == RoleType.Director)
        .Select(cp => new CrewMemberDTO
        {
            Id = cp.Person.Id,
            Name = cp.Person.Name,
            AvatarUrl = cp.Person.AvatarUrl,
            Nationality = cp.Person.Nationality
        }),

                Music = content.ContentPeople
        .Where(cp => cp.RoleType == RoleType.Music)
        .Select(cp => new CrewMemberDTO
        {
            Id = cp.Person.Id,
            Name = cp.Person.Name,
            AvatarUrl = cp.Person.AvatarUrl,
            Nationality = cp.Person.Nationality
        })
            };

            return ApiResponse<ContentDetailDTO>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<IEnumerable<ContentListDTO>>> GetContentList(string? type, string? filter, int limit)
        {
            if (string.IsNullOrWhiteSpace(type) || string.IsNullOrWhiteSpace(filter)) return ApiResponse<IEnumerable<ContentListDTO>>.FailResponse("type and filter required", 400);
            if (!Enum.TryParse<ContentType>(type, true, out var contentType)) return ApiResponse<IEnumerable<ContentListDTO>>.FailResponse("wrong type", 400);

            var validFilters = new[] { "trending", "new-release", "must-watch" };
            if (!validFilters.Contains(filter.ToLower())) return ApiResponse<IEnumerable<ContentListDTO>>.FailResponse("wrong filter", 400);

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
            if (string.IsNullOrWhiteSpace(type)) return ApiResponse<IEnumerable<ContentPageGenreDTO>>.FailResponse("empty error", 400);


            if (!Enum.TryParse<ContentType>(type, true, out var contentType)) return ApiResponse<IEnumerable<ContentPageGenreDTO>>.FailResponse("wrong type, available types: movie, show", 400);

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

            return ApiResponse<IEnumerable<HeroDTO>>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<IEnumerable<ReviewDTO>>> GetReviews(string? id)
        {
            if (!int.TryParse(id, out var result) || result <= 0) return ApiResponse<IEnumerable<ReviewDTO>>.FailResponse("id must be int", 400);

            var reviews = await _contentRepository.GetReviews(result);

            if (reviews == null) return ApiResponse<IEnumerable<ReviewDTO>>.FailResponse("reviews not found", 404);

            var dto = reviews.Select(r => new ReviewDTO
            {
                Id = r.Id,
                ReviewerName = r.ReviewerName,
                ReviewerLocation = r.ReviewerLocation,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                CreatedAt = r.CreatedAt
            });

            return ApiResponse<IEnumerable<ReviewDTO>>.SuccessResponse(dto);
        }

        public async Task<ApiResponse<IEnumerable<SeasonDTO>>> GetSeasons(string? id)
        {
            if (!int.TryParse(id, out var result) || result <= 0) return ApiResponse<IEnumerable<SeasonDTO>>.FailResponse("id must be an int", 400);

            var seasonspilyusepisodes = await _contentRepository.GetSeasons(result);

            if (seasonspilyusepisodes == null) return ApiResponse<IEnumerable<SeasonDTO>>.FailResponse("its null", 404);

            var dtos = seasonspilyusepisodes.Select(s => new SeasonDTO
            {
                Id = s.Id,
                SeasonNumber = s.SeasonNumber,
                EpisodeCount = s.EpisodeCount,
                Title = s.Title,
                Episodes = s.Episodes.Select(e => new EpisodeDTO
                {
                    Id = e.Id,
                    EpisodeNumber = e.EpisodeNumber,
                    Title = e.Title,
                    Description = e.Description,
                    ThumbnailUrl = e.ThumbnailUrl,
                    DurationMinutes = e.DurationMinutes
                })
            });

            return ApiResponse<IEnumerable<SeasonDTO>>.SuccessResponse(dtos);
        }

        public async Task<ApiResponse<IEnumerable<TopTenDTO>>> GetTopTen(string? type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                return ApiResponse<IEnumerable<TopTenDTO>>.FailResponse("empty error", 400);
            }
            if (!Enum.TryParse<ContentType>(type, true, out var mediaType))
            {
                return ApiResponse<IEnumerable<TopTenDTO>>.FailResponse("wrong type, available types: movie, show", 400);
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

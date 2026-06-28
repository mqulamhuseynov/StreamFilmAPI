using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Enums;
using WebApplication1.Repository.Interfaces;

namespace WebApplication1.Repository.Implementations
{
    public class ContentRepository : IContentRepository
    {
        private readonly AppDbContext _context;

        public ContentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Content?> GetContentDetailById(int id)
        {
            return await _context.Contents
                .AsNoTracking()
                .Include(c => c.ContentGenres)
                   .ThenInclude(c => c.Genre)
                .Include(c => c.ContentLanguages)
                .Include(c => c.ContentPeople)
                .ThenInclude(cp => cp.Person)
                .FirstOrDefaultAsync(c => c.Id == id);


        }

        public async Task<IEnumerable<Content>> GetContents(ContentType type, int limit, string flag)
        {
            var query = _context.Contents.AsNoTracking()
                .Where(c => c.Type == type);

            query = flag switch
            {
                "new-release" => query.Where(c => c.IsNewRelease),
            "trending" => query.Where(c => c.IsTrending),
            "must-watch" => query.Where(c=>c.IsMustWatch),
            _ => query
            };

            return await query.Take(limit).ToListAsync();
        }

        public async Task<IEnumerable<Content>> GetFeatured()
        {
            return await _context.Contents.AsNoTracking()
                .Where(f => f.IsFeatured)
                .ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetReviews(int id)
        {
            return await _context.Reviews
                .AsNoTracking()
                .Where(r => r.ContentId == id)
                .OrderByDescending(r => r.Id)
                .ToListAsync();
        }

        public async Task<IEnumerable<Season>> GetSeasons(int id)
        {
            return await _context.Seasons
                .AsNoTracking()
                .Include(c => c.Episodes.OrderBy(c => c.EpisodeNumber))
                .Where(s => s.ContentId == id)
                .OrderBy(c => c.SeasonNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Content>> GetTopTen(ContentType type)
        {
            return await _context.Contents.AsNoTracking()
                .Include(c => c.ContentGenres)
                .ThenInclude(c => c.Genre)
                .Where(c => c.Type == type && c.TopTenRank != null)
                .OrderBy(c => c.TopTenRank)
                .ToListAsync();

        }

        public async Task<bool> ItemExists(int id)
        {
            return await _context.Contents.AnyAsync(c => c.Id == id);
        }
    }
}

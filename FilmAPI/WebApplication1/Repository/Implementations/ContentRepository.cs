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

        public async Task<IEnumerable<Content>> GetTopTen(ContentType type)
        {
            return await _context.Contents.AsNoTracking()
                .Include(c => c.ContentGenres)
                .ThenInclude(c => c.Genre)
                .Where(c => c.Type == type && c.TopTenRank != null)
                .OrderBy(c => c.TopTenRank)
                .ToListAsync();

        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Enums;
using WebApplication1.Repository.Interfaces;

namespace WebApplication1.Repository.Implementations
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Genre>> GetGenres(GenreType? type)
        {
            var query = _context.Genres.AsNoTracking();

            if (type.HasValue && type.Value == GenreType.All) 
            {
                query = query.Where(g => g.Type == type.Value || g.Type == GenreType.All);
            }
            return await query.ToListAsync();
        }
    }
}

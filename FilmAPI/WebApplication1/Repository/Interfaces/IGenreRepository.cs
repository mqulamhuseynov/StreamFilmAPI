using WebApplication1.Entities;
using WebApplication1.Enums;

namespace WebApplication1.Repository.Interfaces
{
    public interface IGenreRepository
    {
        public Task<IEnumerable<Genre>> GetGenres(GenreType? type);
    }
}

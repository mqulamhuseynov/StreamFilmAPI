
using WebApplication1.Entities;
using WebApplication1.Entities.Commons;
using WebApplication1.Enums;

namespace WebApplication1.Repository.Interfaces
{
    public interface IContentRepository
    {
        public Task<IEnumerable<Content>> GetFeatured();
        public Task<IEnumerable<Content>> GetTopTen(ContentType type);
        public Task<IEnumerable<Content>> GetContents(ContentType type, int limit, string flag);
        public Task<Content?> GetContentDetailById(int id);
        public Task<IEnumerable<Review>> GetReviews(int id);
        public Task<IEnumerable<Season>> GetSeasons(int id);
        public Task<bool> ItemExists(int id);
    }
}

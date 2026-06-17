using WebApplication1.Enums;

namespace WebApplication1.Entities
{
    public class Content
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public string BackgroundUrl { get; set; } = null!;
        public string? TrailerUrl { get; set; }
        public ContentType Type { get; set; }
        public int ReleaseYear { get; set; }
        public decimal? ImdbRating { get; set; }
        public decimal? StreamvibeRating { get; set; }
        public bool IsTrending { get; set; }
        public bool IsNewRelease { get; set; }
        public bool IsMustWatch { get; set; }
        public bool IsFeatured { get; set; }
        public int? TopTenRank { get; set; }
        public DateTime CreatedAt { get; set; }
        //nullreferenceexception xetasi almamaq ucun new list yazirig
        public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
        public ICollection<ContentLanguage> ContentLanguages { get; set; } = new List<ContentLanguage>();
        public ICollection<Season> Seasons { get; set; } = new List<Season>();
        public ICollection<ContentPerson> ContentPeople { get; set; } = new List<ContentPerson>();
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}

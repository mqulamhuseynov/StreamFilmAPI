using WebApplication1.Enums;

namespace WebApplication1.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Slug { get; set; } = null!;
        public GenreType Type { get; set; }
        public string? CoverImage1 { get; set; }
        public string? CoverImage2 { get; set; }
        public string? CoverImage3 { get; set; }
        public string? CoverImage4 { get; set; }

        public ICollection<ContentGenre> ContentGenres { get; set; } = new List<ContentGenre>();
    }
}


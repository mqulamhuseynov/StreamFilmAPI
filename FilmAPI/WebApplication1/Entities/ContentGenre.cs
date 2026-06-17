namespace WebApplication1.Entities
{
    public class ContentGenre
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; } = null!;
        public int GenreId { get; set; }
        public Genre Genre { get; set; } = null!;
    }
}

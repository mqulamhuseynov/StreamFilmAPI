namespace WebApplication1.Service.DTOs
{
    public class ContentListDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public decimal? ImdbRating { get; set; }
        public decimal? StreamvibeRating { get; set; }
        public int ReleaseYear { get; set; }
        public string Type { get; set; } = null!;
    }
}

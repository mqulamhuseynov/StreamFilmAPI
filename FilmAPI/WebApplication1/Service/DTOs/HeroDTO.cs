namespace WebApplication1.Service.DTOs
{
    public class HeroDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string BackgroundUrl { get; set; } = null!;
        public string? TrailerUrl { get; set; }
        public string Type { get; set; } = null!;
    }
}

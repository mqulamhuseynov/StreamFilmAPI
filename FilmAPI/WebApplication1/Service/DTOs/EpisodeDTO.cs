namespace WebApplication1.Service.DTOs
{
    public class EpisodeDTO
    {
        public int Id { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int DurationMinutes { get; set; }
    }
}

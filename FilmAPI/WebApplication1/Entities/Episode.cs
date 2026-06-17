namespace WebApplication1.Entities
{
    public class Episode
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; } = null!;
        public int EpisodeNumber { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ThumbnailUrl { get; set; }
        public int DurationMinutes { get; set; }
    }
}

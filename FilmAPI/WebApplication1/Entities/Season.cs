namespace WebApplication1.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; } = null!;
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }
        public string? Title { get; set; }

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
    }
}

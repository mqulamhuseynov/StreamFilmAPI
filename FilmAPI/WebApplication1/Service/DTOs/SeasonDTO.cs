namespace WebApplication1.Service.DTOs
{
    public class SeasonDTO
    {
        public int Id { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeCount { get; set; }
        public string? Title { get; set; }
        public IEnumerable<EpisodeDTO> Episodes { get; set; } = null!;
    }
}

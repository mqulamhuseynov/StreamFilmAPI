namespace WebApplication1.Service.DTOs
{
    public class ContentDetailDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public string BackgroundUrl { get; set; } = null!;
        public string? TrailerUrl { get; set; }
        public string Type { get; set; } = null!;
        public int ReleaseYear { get; set; }
        public decimal? ImdbRating { get; set; }
        public decimal? StreamvibeRating { get; set; }
        public IEnumerable<ContentDetailGenreDTO> Genres { get; set; } = null!;
        public IEnumerable<string> Languages { get; set; } = null!;
        public IEnumerable<CastMemberDTO> Cast { get; set; } = null!;
        public IEnumerable<CrewMemberDTO> Directors { get; set; } = null!;
        public IEnumerable<CrewMemberDTO> Music { get; set; } = null!;
    }
}

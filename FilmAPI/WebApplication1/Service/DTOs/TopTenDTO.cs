namespace WebApplication1.Service.DTOs
{
    public class TopTenDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string PosterUrl { get; set; } = null!;
        public int TopTenRank { get; set; }
        public IEnumerable<TopTenGenreDTO> Genres { get; set; } = null!;
    }
}

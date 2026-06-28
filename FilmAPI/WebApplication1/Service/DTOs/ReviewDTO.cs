namespace WebApplication1.Service.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = null!;
        public string? ReviewerLocation { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

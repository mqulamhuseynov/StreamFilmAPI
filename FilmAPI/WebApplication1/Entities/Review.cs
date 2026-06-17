namespace WebApplication1.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; } = null!;
        public string ReviewerName { get; set; } = null!;
        public string? ReviewerLocation { get; set; }
        public decimal Rating { get; set; }
        public string ReviewText { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}

namespace WebApplication1.Entities
{
    public class ContentLanguage
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; } = null!;
        public string Language { get; set; } = null!;
    }
}

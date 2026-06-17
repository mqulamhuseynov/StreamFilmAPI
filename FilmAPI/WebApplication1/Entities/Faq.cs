namespace WebApplication1.Entities
{
    public class Faq
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public int OrderNumber { get; set; }
    }
}

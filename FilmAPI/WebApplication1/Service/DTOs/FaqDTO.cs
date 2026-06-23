namespace WebApplication1.Service.DTOs
{
    public class FaqDTO
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public string Answer { get; set; } = null!;
        public int OrderNumber { get; set; }
    }
}

namespace WebApplication1.Service.DTOs
{
    public class CrewMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string? Nationality { get; set; }
    }
}

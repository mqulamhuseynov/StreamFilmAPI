namespace WebApplication1.Service.DTOs
{
    public class CastMemberDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string? Nationality { get; set; }
        public string? CharacterName { get; set; }
    }
}

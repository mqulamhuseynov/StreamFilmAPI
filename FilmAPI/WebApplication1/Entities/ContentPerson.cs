using WebApplication1.Enums;

namespace WebApplication1.Entities
{
    public class ContentPerson
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public Content Content { get; set; } = null!;
        public int PersonId { get; set; }
        public Person Person { get; set; } = null!;
        public RoleType RoleType { get; set; }
        public string? CharacterName { get; set; }
    }
}

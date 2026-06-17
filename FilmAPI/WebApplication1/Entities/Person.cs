namespace WebApplication1.Entities
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? AvatarUrl { get; set; }
        public string? Nationality { get; set; }

        public ICollection<ContentPerson> ContentPeople { get; set; } = new List<ContentPerson>();
    }
}

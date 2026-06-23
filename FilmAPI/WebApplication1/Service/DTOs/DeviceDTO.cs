namespace WebApplication1.Service.DTOs
{
    public class DeviceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string? IconName { get; set; }
    }
}

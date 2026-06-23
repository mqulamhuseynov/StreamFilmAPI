using WebApplication1.Entities;

namespace WebApplication1.Repository.Interfaces
{
    public interface IDeviceRepository
    {
        public Task<IEnumerable<Device>> GetDevices();
    }
}

using WebApplication1.Entities.Commons;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;

        public DeviceService(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }

        public async Task<ApiResponse<IEnumerable<DeviceDTO>>> GetDevices()
        {
            var devices = await _deviceRepository.GetDevices();

            var dto = devices.Select(d => new DeviceDTO 
            {
                Id = d.Id,
                Name = d.Name,
                Description = d.Description,
                IconName = d.IconName
            }).ToList();

            return ApiResponse<IEnumerable<DeviceDTO>>.SuccessResponse(dto);
        }
    }
}

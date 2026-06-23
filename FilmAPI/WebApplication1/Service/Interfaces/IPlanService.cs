using System.Security.Cryptography.X509Certificates;
using WebApplication1.Entities.Commons;
using WebApplication1.Service.DTOs;

namespace WebApplication1.Service.Interfaces
{
    public interface IPlanService
    {
        public Task<ApiResponse<IEnumerable<PlanDTO>>> GetPlans(string? plan);
    }
}

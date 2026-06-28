using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Entities.Commons;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations
{
    public class PlanService : IPlanService
    {
       private readonly IPlanRepository _planRepository;

        public PlanService(IPlanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task<ApiResponse<IEnumerable<PlanDTO>>> GetPlans(string? plan)
        {
            var billing = string.IsNullOrWhiteSpace(plan) ? "monthly" : plan.ToLower();

            if (plan != "monthly" && plan != "yearly") 
            {
                return ApiResponse<IEnumerable<PlanDTO>>.FailResponse("Plan is wrong, available plans are: monthly,yearly", 400);
            }

            var plans = await _planRepository.GetPricingPlans();

            var dto = plans.Select(p => new PlanDTO
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = billing == "monthly" ? p.PriceMonthly : p.PriceYearly,
                IsPopular = p.IsPopular
            });

            return ApiResponse<IEnumerable<PlanDTO>>.SuccessResponse(dto);
        }
    }
}

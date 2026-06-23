using WebApplication1.Entities;

namespace WebApplication1.Repository.Interfaces
{
    public interface IPlanRepository
    {
        public Task<IEnumerable<PricingPlan>> GetPricingPlans();
    }
}

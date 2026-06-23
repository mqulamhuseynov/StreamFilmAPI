using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Repository.Interfaces;

namespace WebApplication1.Repository.Implementations
{
    public class PlanRepository : IPlanRepository
    {
        private readonly AppDbContext _context;

        public PlanRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PricingPlan>> GetPricingPlans()
        {
            return await _context.PricingPlans.AsNoTracking()
                .ToListAsync();
        }
    }
}

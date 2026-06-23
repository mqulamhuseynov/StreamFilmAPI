using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Entities;
using WebApplication1.Repository.Interfaces;

namespace WebApplication1.Repository.Implementations
{
    public class FaqRepository : IFaqRepository
    {
        private readonly AppDbContext _context;

        public FaqRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Faq>> GetQuestions()
        {
            return await _context.Faqs.AsNoTracking()
                .OrderBy(o => o.OrderNumber)
                .ToListAsync();
        }
    }
}

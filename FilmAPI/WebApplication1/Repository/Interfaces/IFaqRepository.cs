using WebApplication1.Entities;

namespace WebApplication1.Repository.Interfaces
{
    public interface IFaqRepository
    {
        public Task<IEnumerable<Faq>> GetQuestions();
    }
}

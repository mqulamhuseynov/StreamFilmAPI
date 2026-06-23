using WebApplication1.Entities.Commons;
using WebApplication1.Service.DTOs;

namespace WebApplication1.Service.Interfaces
{
    public interface IFaqService
    {
        public Task<ApiResponse<IEnumerable<FaqDTO>>> GetQuestions();
    }
}

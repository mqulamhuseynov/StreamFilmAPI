using WebApplication1.Entities.Commons;
using WebApplication1.Repository.Interfaces;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Service.Implementations
{
    public class FaqService : IFaqService
    {
        private readonly IFaqRepository _faqRepository;

        public FaqService(IFaqRepository faqRepository)
        {
            _faqRepository = faqRepository;
        }

        public async Task<ApiResponse<IEnumerable<FaqDTO>>> GetQuestions()
        {
            var faqs = await _faqRepository.GetQuestions();

            var dtos = faqs.Select(f => new FaqDTO
            {
                Id = f.Id,
                Question = f.Question,
                Answer = f.Answer,
                OrderNumber = f.OrderNumber
            }).ToList();

            return ApiResponse<IEnumerable<FaqDTO>>.SuccessResponse(dtos);
        }


    }
}

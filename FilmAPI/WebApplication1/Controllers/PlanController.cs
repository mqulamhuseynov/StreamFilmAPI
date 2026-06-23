using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlanController : ControllerBase
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPlansAsync([FromQuery] string? billing = "monthly") 
        {
            var plans = await _planService.GetPlans(billing);
            return plans.Success ? Ok(plans) : BadRequest(plans);
        }
    }
}

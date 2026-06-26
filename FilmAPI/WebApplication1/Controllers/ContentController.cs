using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetHero() 
        {
            var heros = await _contentService.GetHero();
            return Ok(heros);
        }

        [HttpGet]
        public async Task<IActionResult> GetTopTen([FromQuery]string? type) 
        {
            var toptens = await _contentService.GetTopTen(type);
            return toptens.Success ? Ok(toptens) : BadRequest(toptens);
        }

        [HttpGet]
        public async Task<IActionResult> GetContentPageGenre([FromQuery] string? type) 
        {
        var pagegenre = await _contentService.GetContentPageGenre(type);
            return pagegenre.Success ? Ok(pagegenre) : BadRequest(pagegenre);
        }

        [HttpGet]
        public async Task<IActionResult> GetContentList([FromQuery] string? type, [FromQuery] string? filter, [FromQuery] int limit = 10) 
        {
        var contentlist = await _contentService.GetContentList(type, filter, limit);
            return contentlist.Success ? Ok(contentlist) : BadRequest(contentlist);
        }
    }
}

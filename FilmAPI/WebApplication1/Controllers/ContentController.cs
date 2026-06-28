using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Entities;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/content")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        [HttpGet("hero")]
        public async Task<IActionResult> GetHero()
        {
            var heros = await _contentService.GetHero();
            return Ok(heros);
        }

        [HttpGet("top-ten")]
        public async Task<IActionResult> GetTopTen([FromQuery] string? type)
        {
            var toptens = await _contentService.GetTopTen(type);
            return toptens.Success ? Ok(toptens) : BadRequest(toptens);
        }

        [HttpGet("genres")]
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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentDetail([FromRoute] string? id)
        {
            var content = await _contentService.GetContentDetail(id);

            return content.StatusCode switch
            {
                404 => NotFound(content),
                400 => BadRequest(content),
                _ => Ok(content)
            };
        }
        //string id isledirik cunki 400 ve 404 errorlarini ozumuz ayird ede bilek deye
        [HttpGet("{id}/reviews")]
        public async Task<IActionResult> GetReviews([FromRoute] string? id) 
        {
        var reviews = await _contentService.GetReviews(id);

            return reviews.StatusCode switch
            {
                404 => NotFound(reviews),
                400 => BadRequest(reviews),
                _ => Ok(reviews)
            };
        }

        [HttpGet("{id}/seasons")]
        public async Task<IActionResult> GetSeasons([FromRoute] string? id)
        {
            var seasonspilyusepisodes = await _contentService.GetSeasons(id);

            return seasonspilyusepisodes.StatusCode switch
            {
                404 => NotFound(seasonspilyusepisodes),
                400 => BadRequest(seasonspilyusepisodes),
                _ => Ok(seasonspilyusepisodes)
            };
        


        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Superpower.Model;
using WebApplication1.Service.DTOs;
using WebApplication1.Service.Interfaces;

namespace WebApplication1.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO dto) 
        {
            var register = await _auth.Register(dto);

            return register.StatusCode switch
            {
                201 => StatusCode(201, register),
                409 => Conflict(register),
                400 => BadRequest(register),
                _ => Ok(register)
            };
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO dto) 
        {
        var login = await _auth.Login(dto);
            return login.StatusCode switch
            {
                401 => Unauthorized(login),
                403 => StatusCode(403, login),
                400 => BadRequest(login),
                _ => Ok(login)
            };
        }
    }
}

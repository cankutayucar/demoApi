using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public IActionResult register([FromForm]AuthDto authDto)
        {
           var result = _authService.Register(authDto);
           return result.Success ? Ok(result.Message) : BadRequest(result.Message);
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginAuthDto authDto)
        {
            var result = _authService.Login(authDto);
            if (result.Success) return Ok(result.Data); 
            return BadRequest(result.Message);
        }
    }
}

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
        public IActionResult register(AuthDto authDto)
        {
            _authService.Register(authDto);
            return Ok("kullanıcı kayıt işlemi başarılı");
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginAuthDto authDto)
        {
            return Ok(_authService.Login(authDto));
        }
    }
}

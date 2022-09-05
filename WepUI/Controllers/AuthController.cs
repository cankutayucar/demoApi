using System.Security.Claims;
using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace WepUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginAuthDto authDto)
        {
            var result = _authService.Login(authDto);
            if (result.Success)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, "Cankutay Uçar")
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties();
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity), authProperties);
                return Redirect("/Home/Index");
            }

            TempData["Hata"] = result.Message;
            return Redirect("/Login/Auth");
        }


    }
}

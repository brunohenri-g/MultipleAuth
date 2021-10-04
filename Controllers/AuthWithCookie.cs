using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MultipleAuth.Controllers
{
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class AuthWithCookie : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> ValidarAuth()
        {
            var user = HttpContext.User;

            return Ok($"You can access: { user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value }");
        }

        [HttpGet("Account/Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login()
        {
            var userClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, "MeuUser"),
                new Claim(ClaimTypes.Email, "meu@email.com"),
            };

            var minhaIdentity = new ClaimsIdentity(userClaims, "Usuario");
            var userPrincipal = new ClaimsPrincipal(new[] { minhaIdentity });


            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);

            return Ok("Do Login first");
        }
    }
}

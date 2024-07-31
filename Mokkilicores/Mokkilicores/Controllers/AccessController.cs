using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Mokkilicores.Models;
using Microsoft.Extensions.Caching.Memory;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Mokkilicores.Data;

namespace Mokkilicores.Controllers
{
    public class AccessController : Controller
    {

        private readonly IMemoryCache _cache;

        public AccessController(IMemoryCache cache)
        {
            _cache = cache;
        }
        public IActionResult Login()
        {

            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
                return RedirectToAction("Index", "Home");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(MkLogin modelLogin)
        {
            Conexion datos = new Conexion(_cache);
            List<Cliente> tempList = datos.GetCliente();


            if (tempList != null || tempList.Count > 0)
            {
                foreach (Cliente cliente in tempList)
                {

                    if ((modelLogin.User == cliente.Id && modelLogin.Password == cliente.PassWord) || (modelLogin.User == "user@example.com" && modelLogin.Password == "123"))
                    {

                        List<Claim> claims = new List<Claim>() {

                    new Claim(ClaimTypes.NameIdentifier, modelLogin.User),
                    new Claim("OtherProperties", "Example Role")

                };

                        ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                        AuthenticationProperties properties = new AuthenticationProperties()
                        {

                            AllowRefresh = true,
                            IsPersistent = modelLogin.KeepLoggedIn

                        };

                        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                            new ClaimsPrincipal(claimsIdentity), properties);

                        return RedirectToAction("Index", "Home");

                    }
                }

                ViewData["ValidateMessage"] = "Usuario no encontrado";
            }

            return View();
        }
    }
}

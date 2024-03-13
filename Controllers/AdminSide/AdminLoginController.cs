using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using e_commerce_pro.Models;

namespace e_commerce_pro.Controllers
{
    public class AdminLoginController : Controller
    {

        public IActionResult ALogin()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Dashboard", "Dashboard");
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public async Task<IActionResult> ALogin(Admin model)
        {
            if (IsValidUser(model.Username, model.Password))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "AdminRole")

                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity));

                return RedirectToAction("Dashboard", "Dashboard");
            }

            ViewData["ValidateMessage"] = "Invalid credentials";
            return View();
        }
        private bool IsValidUser(string username, string password) =>
            username == "anshifkp" && password == "12345";
    }
}

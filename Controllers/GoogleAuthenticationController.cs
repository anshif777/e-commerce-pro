using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_pro.Controllers
{
    public class GoogleAuthenticationController : Controller
    {
        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties
                {
                    RedirectUri = Url.Action("GoogleResponse")
                });
        }
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.
                AuthenticationScheme);
            var Claims = result.Principal.Identities.FirstOrDefault().Claims.Select(cliam => new
            {
                cliam.Issuer,
                cliam.OriginalIssuer,
                cliam.Type,
                cliam.Value
            });
            return Json(Claims);
        }
    }
}

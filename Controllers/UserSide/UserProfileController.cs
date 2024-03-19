using e_commerce_pro.Data;
using e_commerce_pro.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_pro.Controllers.UserSide
{
    public class UserProfileController : Controller
    {
        private readonly ApplicationDb _db;
        public UserProfileController(ApplicationDb db)
        {
            _db = db;
        }
        public IActionResult UserProfilDash()
        {
            if (HttpContext.Session.GetString("session") != null)
            {

                return View();
            }
            return RedirectToAction("Login", "USingup");

        }
        public IActionResult UserProfil()
        {
            return View();
        }

    }
}

using e_commerce_pro.Data;
using e_commerce_pro.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace e_commerce_pro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDb _db;
        public HomeController(ILogger<HomeController> logger , ApplicationDb db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult MainHome()
        {
            List<Products> products = _db.products.Include(p => p.CategorieS).ToList();

            return View(products);
        }

        public IActionResult Productdetails(int id)
        {
            var check = _db.products.Find(id);

            if (check == null)
            {
                return RedirectToAction("MainHome");
            }

            return View(check);
        }
        public IActionResult LogOut()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                // Remove the session variable that represents authentication
                HttpContext.Session.Remove("session");

                // Redirect to the main home page after successful logout
                return RedirectToAction("MainHome", "Home");
            }
            // If session variable is already null, just return to a view
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

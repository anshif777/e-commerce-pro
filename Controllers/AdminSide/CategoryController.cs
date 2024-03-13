using e_commerce_pro.Data;
using e_commerce_pro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_pro.Controllers.AdminSide
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ApplicationDb _db;
        public CategoryController(ApplicationDb db)
        {
            _db = db;
        }
        public IActionResult Categories()
        {
            List<CategorieS> Objcatagory = _db.categories.ToList();

            return View(Objcatagory);
        }

        [HttpPost]
        public IActionResult AddCatogary(CategorieS Cg)
        {

            if (ModelState.IsValid)
            {
                _db.categories.Add(Cg);
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View();
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();

            }
            var user = _db.categories.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }
        [HttpPost]
        public IActionResult Edit(CategorieS Cg)
        {

            if (ModelState.IsValid)
            {
                _db.categories.Update(Cg);
                _db.SaveChanges();
                return RedirectToAction("Categories");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Unlisted(int ? id)
        {
           if (id == null)
            {
                return NotFound();
            }
            CategorieS ? user = _db.categories.Find(id);
            if (user != null)
            {
                user.IsList = false;
                _db.SaveChanges();

                return RedirectToAction("Categories");

            }
            return NotFound();


        }
        [HttpPost]
        public IActionResult Listed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            CategorieS? user = _db.categories.Find(id);
            if (user != null)
            {
                user.IsList = true;
                _db.SaveChanges();

                return RedirectToAction("Categories");

            }
            return NotFound();


        }
    }
}

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
        //Category List
        public IActionResult CategoryList()
        {
            List<CategorieS> Objcatagory = _db.categories.ToList();

            return View(Objcatagory);
        }
        //Add New Category
        [HttpPost]
        public IActionResult AddCatogary(CategorieS Cg)
        {
            if (ModelState.IsValid)
            {
                var  user = _db.categories.Where(c => c.name == Cg.name).FirstOrDefault();
                if (user == null) 
                {
                    _db.categories.Add(Cg);
                    _db.SaveChanges();
                    return RedirectToAction("CategoryList");
                }
            }
            return View();
        }
        //Edit Category(Get)
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
        //Edit Category (Post)
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
         //Unblock Block Category from Database
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

                return RedirectToAction("CategoryList");

            }
            return NotFound();


        }
        //Block Category from Database   
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

                return RedirectToAction("CategoryList");

            }
            return NotFound();


        }
    }
}

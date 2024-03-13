using e_commerce_pro.Data;
using e_commerce_pro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace e_commerce_pro.Controllers.AdminSide
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly ApplicationDb _db;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDb db , IWebHostEnvironment webHostEnvironment)
        {
            _db = db;
            _webHostEnvironment = webHostEnvironment;
        }
        [HttpGet]
        public IActionResult ProdectList()
        { 

            List<Products> products = _db.products.Include(p => p.CategorieS).ToList();

            return View(products);
        }
        [HttpGet]
        public IActionResult CreateProduct()
        {

            List<CategorieS> categoryList = _db.categories.Where(c => !c.IsList).ToList();// Replace 'Category' with the actual name of your category model

            // You can use ViewBag or ViewData to pass the category list to the view

            ViewBag.CategoryList = new SelectList(categoryList , "Id" , "name");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Products Vm, IFormFile? img1, IFormFile? img2, IFormFile? img3, IFormFile? img4)
        {
            if (ModelState.IsValid)
            {
                // Process product image uploads asynchronously
                var imageUrlTasks = new List<Task<string>>();

                imageUrlTasks.Add(ProcessImageAsync(img1));
                imageUrlTasks.Add(ProcessImageAsync(img2));
                imageUrlTasks.Add(ProcessImageAsync(img3));
                imageUrlTasks.Add(ProcessImageAsync(img4));

                // Wait for all image uploads to complete
                await Task.WhenAll(imageUrlTasks);

                // Retrieve the results from the completed tasks
                List<string> imageUrls = imageUrlTasks.Select(task => task.Result).ToList();

                // Assign the list of image URLs to the product's property
                Vm.imagUrl = imageUrls;

                // Add the product to the database
                _db.products.Add(Vm);

                // Save changes to the database
                await _db.SaveChangesAsync();

                return RedirectToAction("ProdectList");
            }

            return View();
        }
        private async Task<string> ProcessImageAsync(IFormFile? image)
        {
            if (image != null)
            {
                string wwRootpath = _webHostEnvironment.WebRootPath;

                // Generate a unique file name for the image
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                string productPath = Path.Combine(wwRootpath, @"images/product");

                // Save the image to the server asynchronously
                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    await image.CopyToAsync(fileStream);
                }

                // Return the URL of the saved image
                return "/images/product/" + fileName;
            }

            return null; // or throw an exception, depending on your requirements
        }
        public IActionResult EditProdect(int ?id)
        
        {
            // Retrieve the product from the database based on the productId
            Products? product = _db.products.Find(id);

            // Check if the product is found
            if (product == null)
            {
                return NotFound(); // or handle accordingly, e.g., show an error view
            }

            List<CategorieS> categoryList = _db.categories.ToList(); // Replace 'Category' with the actual name of your category model

            // You can use ViewBag or ViewData to pass the category list to the view
            ViewBag.CategoryList = new SelectList(categoryList , "Id" , "name");

            return View(product);
        }
        [HttpPost]
        public async Task<IActionResult> EditProdect(Products product, IFormFile? img1, IFormFile? img2, IFormFile? img3, IFormFile? img4)
        {
            if (ModelState.IsValid)
            {
                // Process product image uploads asynchronously
                var imageUrlTasks = new List<Task<string>>();

                imageUrlTasks.Add(ProcessImageAsync(img1));
                imageUrlTasks.Add(ProcessImageAsync(img2));
                imageUrlTasks.Add(ProcessImageAsync(img3));
                imageUrlTasks.Add(ProcessImageAsync(img4));

                // Wait for all image uploads to complete
                await Task.WhenAll(imageUrlTasks);

                // Retrieve the results from the completed tasks
                List<string> imageUrls = imageUrlTasks.Select(task => task.Result).ToList();

                // Assign the list of image URLs to the product's property
                product.imagUrl = imageUrls;

                // Update the existing product in the database
                _db.products.Update(product);

                // Save changes to the database
                await _db.SaveChangesAsync();

                return RedirectToAction("ProdectList");
            }

            // Repopulate the ViewBag or ViewData with the category list if needed
            // ViewBag.CategoryList = GetCategoryList();

            return View(product);
        }
        [HttpPost]
        public IActionResult Unlisted(int ? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Products? product = _db.products.Find(id);
            if (product != null)
            {
                product.Islisted = false;
                _db.SaveChanges();
                return RedirectToAction("ProdectList");
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
            Products? product = _db.products.Find(id);
            if (product != null)
            {
                product.Islisted = true;
                _db.SaveChanges();
                return RedirectToAction("ProdectList");
            }
            return NotFound();

        }

    }
}

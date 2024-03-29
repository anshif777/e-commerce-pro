﻿using e_commerce_pro.Data;
using e_commerce_pro.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;


namespace e_commerce_pro.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDb _db;
        
        public DashboardController(ApplicationDb db)
        {
            _db = db;
           
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("ALogin", "AdminLogin");

        }
        [Route("/Userlist")]
        public IActionResult UserList()
        {
            List<UserSingup> Objuserlist = _db.Usersingup.ToList();

            return View(Objuserlist);
        }
        // Action to block a user
        [HttpPost]
        public IActionResult BlockUser(int? id)
        {
            var user = _db.Usersingup.Find(id);

            if (user != null)
            {
                user.IsBlock = true;
                _db.SaveChanges();
                return RedirectToAction("UserList", "Dashboard");
                // Redirect to user management page
            }

            // Handle the case where the user is not found
            return NotFound();
        }
        // Action to unblock a user
        [HttpPost]
        public IActionResult UnblockUser(int? id)
        {
            var user = _db.Usersingup.Find(id);

            if (user != null)
            {
                user.IsBlock = false;
                _db.SaveChanges();
                return RedirectToAction("UserList", "Dashboard");

            }
            // Handle the case where the user is not found
            return NotFound();
        }
    }
}

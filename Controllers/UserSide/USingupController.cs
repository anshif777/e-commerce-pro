using e_commerce_pro.Data;
using e_commerce_pro.Models;
using e_commerce_pro.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_pro.Controllers
{
    public class USingupController : Controller
    {
        private readonly ApplicationDb _db;
        private readonly IEmailService _emailService;
        public USingupController(ApplicationDb db, IEmailService emailservice)
        {
            this._db = db;
            this._emailService = emailservice;

        }
        [Route("/Singup")]
        [HttpGet]
        public IActionResult Singup()
        {
            return View();
        }
        [Route("/Singup")]
        [HttpPost]
        public IActionResult Singup(UserSingup sg)
        {
            if (ModelState.IsValid)
            {
                //Check if email already exists
                //var existingEmail = _db.Usersingup.FirstOrDefault(u => u.Email == sg.Email);

                //if (existingEmail != null)
                //{
                //    ViewBag.email = "This Email is already taken";
                //    return View("Singup", sg);
                //}

                // Generate OTP
                var otp = _emailService.GanerateOtpAsync().Result; // Generate OTP synchronously for simplicity


                /// Include OTP in email message
                string emailSubject = "OTP Verification";
                string emailMessage = $"Your OTP is: {otp}. Please use this code to verify your account.";

                try
                {
                    // Send email with OTP
                    _emailService.EmailSenderAsync(sg.Email, emailSubject, emailMessage).Wait();

                    // Save OTP details to database
                    var otpDetails = new Otp
                    {
                        Email = sg.Email,
                        OTp = otp,
                        expertime = DateTime.UtcNow.AddMinutes(1) // Example: OTP expires in 1 minutes

                    };
                    _db.OTPinfo.Add(otpDetails);
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    // Log and handle any exceptions
                    Console.WriteLine($"Error sending email: {ex.Message}");
                    ModelState.AddModelError("ee", "Error sending email. Please try again later.");
                    return View("Singup", sg);
                }

                HttpContext.Session.SetString("JustSignedUp", "true");

                _db.Usersingup.Add(sg);
                _db.SaveChanges();
                // Redirect to OTP verification page
                return RedirectToAction("VerifyOTp", "Otp", new { email = sg.Email });

            }
            return View(sg);
        }
        [Route("/signIn")]
        [HttpGet]
        public IActionResult Login()
        {
            if (HttpContext.Session.GetString("session") != null)
            {
                return RedirectToAction("MianHome", "Home");
            }
            return View();
        }
        [Route("/signIn")]
        [HttpPost]
        public IActionResult Login(Login lg)
        {
            var user = _db.Usersingup.Where(u=>u.Email==lg.Email && u.Password ==lg.Password).FirstOrDefault();

            if(user != null)
            {
                 if (user.IsBlock)
                {
                    // User is blocked, you can redirect to a blocked page or show a message
                    ViewData["BlockedMessage"] = "Your account is blocked. Please contact support for assistance";
                    return View();
                }
                HttpContext.Session.SetString("session" , user.Email);
                return RedirectToAction("MainHome", "Home");
            }
            ViewData["LoginMessage"] = "User not found";

            return View();
        }
       
    }
}
    

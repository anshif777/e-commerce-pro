using e_commerce_pro.Data;
using e_commerce_pro.Models;
using e_commerce_pro.Services;
using Microsoft.AspNetCore.Mvc;

namespace e_commerce_pro.Controllers
{
    public class OtpController : Controller
    {
        private readonly ApplicationDb _db;
        private readonly IEmailService _emailService;
        public OtpController(ApplicationDb db , IEmailService emailService)
        {
           this._db = db;
            this._emailService = emailService;
        }
        [Route("/otp")]
        public IActionResult VerifyOTp(string email)
        {
            TempData["UserEmail"] = email; // Pass the email to the view
            return View();

        }
        [Route("/otp")]
        // Otp verification 
        [HttpPost]
        public IActionResult VerifyOTp(string email, string otp1, string otp2, string otp3, string otp4)
        {
            string otp = otp1 + otp2 + otp3 + otp4;

            var currentUtcTime = DateTime.UtcNow;

            var OTP = _db.OTPinfo.FirstOrDefault(x => x.Email == email && x.OTp == otp && x.expertime > currentUtcTime);

            if (OTP != null)
            {
                // OTP is valid, remove it from the database
                _db.OTPinfo.Remove(OTP);
                _db.SaveChanges();

                TempData["success"] = "OTP verification is successful";
                return RedirectToAction("MainHome", "Home");
            }
            else
            {
                // OTP is not valid or has expired
                if (_db.OTPinfo.Any(x => x.Email == email && x.OTp == otp && x.expertime <= currentUtcTime))
                {
                    ViewBag.expiredmessage = "The entered OTP has expired. Please request a new OTP.";
                }
                else
                {
                    ViewBag.errormessage = "Invalid OTP. Please try again ";
                }

                return View();
            }
        }

        [Route("/ResendOtp")]
        [HttpPost]
        public IActionResult ResendOtp(string email, UserSingup sg)
        {
            var newotp = _emailService.GanerateOtpAsync().Result;

            /// Include OTP in email message
            string emailSubject = "OTP Verification";
            string emailMessage = $"Your OTP is: {newotp}. Please use this code to verify your account.";
            try
            {
                // Send email with OTP
                _emailService.EmailSenderAsync(email, emailSubject, emailMessage).Wait();

                // Save OTP details to database
                var newotpdeatels = new Otp
                {
                    Email = sg.Email,
                    OTp = newotp,
                    expertime = DateTime.UtcNow.AddMinutes(5) // Example: OTP expires in 1 minutes

                };
                _db.OTPinfo.Add(newotpdeatels);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log and handle any exceptions
                Console.WriteLine($"Error sending email: {ex.Message}");
                ModelState.AddModelError("ee", "Error sending email. Please try again later.");
                return View("Singup");
            }
            return RedirectToAction("VerifyOTp", new { email = sg.Email });
        }
    }
}

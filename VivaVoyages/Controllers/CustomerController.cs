using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity.UI.Services;
using VivaVoyages.Models;
using IEmailSender = Microsoft.AspNetCore.Identity.UI.Services.IEmailSender;

namespace VivaVoyages.Controllers
{
    public class CustomerController : Controller
    {
        private readonly VivaVoyagesContext _db;
        private readonly IEmailSender _emailSender;

        public CustomerController(VivaVoyagesContext db, IEmailSender emailSender)
        {
            _db = db;
            _emailSender = emailSender;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(Customer obj)
        {
            if (ModelState.IsValid)
            {
                // Check if the email is already in use

                // Add the new customer to the database
                var isExists = _db.Customers.Any(c => c.Email == obj.Email);
                if (isExists)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                    return View(obj);
                }
                _db.Customers.Add(obj);
                _db.SaveChanges();

                // Redirect to the login page or any other desired page
                return RedirectToAction("Login");
            }

            // If ModelState is not valid, return to the registration view with validation errors
            return View(obj);
        }

        public IActionResult Login()
        {
            // TODO: Your code here
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == email && c.Password == password);

            if (customer != null)
            {
                // Successful login, redirect to home or dashboard
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If login fails, return to login page with an error
                ViewBag.Error = "Email or password is wrong";
                return View();
            }
        }

        [HttpPost]
        public JsonResult emailIsUnique(string email)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                return Json(new { emailExists = true });
            }
            else
            {
                return Json(new { emailExists = false });
            }
        }

        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            // TODO: Your code here
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public IActionResult ForgotPassword(string email)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == email);
            if (customer != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                SendEmail(email, "Reset Code", resetCode);
            }
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
            return View();
        }

        public IActionResult ResetPassword()
        {
            // TODO: Your code here
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string resetCodeCus, string newPassword)
        {
            // Lấy email từ TempData
            string email = TempData["ResetPasswordEmail"] as string;
            string resetCode = TempData["ResetCode"] as string;

            if (email != null && resetCode != null && newPassword != null)
            {
                var customer = _db.Customers.FirstOrDefault(c => c.Email == email);
                if (customer != null)
                {
                    if (resetCodeCus == resetCode) // Thay bằng cách kiểm tra reset code hợp lệ
                    {
                        customer.Password = newPassword;
                        _db.SaveChanges();
                        return RedirectToAction("Login");
                    }
                    else
                    {
                        ViewBag.Error = "Reset code is wrong";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error = "Email not found";
                    return View();
                }
            }

            return View();
        }

    }
}
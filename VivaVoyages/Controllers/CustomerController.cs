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
using System.Data;

namespace VivaVoyages.Controllers
{
    public class CustomerController : Controller
    {
        private readonly VivaVoyagesContext _db;
        private readonly IEmailSender _emailSender;
        private string Email = "";

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
            this.Email = email;
            if (customer != null)
            {
                string resetCode = Guid.NewGuid().ToString();
                customer.resetCode = resetCode;
                _db.SaveChanges();
                SendEmail(email, "Reset Code", resetCode);

                return RedirectToAction("ResetPassword");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendEmail(string email, string subject, string message)
        {
            await _emailSender.SendEmailAsync(email, subject, message);
            return View();
        }

        public IActionResult ResetPassword()
        {

            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(ForgotPassword forgotPassword)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == forgotPassword.Email);
            if (customer != null)
            {
                if (customer.resetCode == forgotPassword.ResetCode) // Thay bằng cách kiểm tra reset code hợp lệ
                {
                    customer.Password = forgotPassword.NewPassword;
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


    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
    public class LoginRegisterController(VivaVoyagesContext db, IEmailSender emailSender) : Controller
    {
        private readonly VivaVoyagesContext _db = db;
        private readonly IEmailSender _emailSender = emailSender;
        private string Email = "";

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
                HttpContext.Session.SetObject("LoggedInCustomer", customer);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                // If login fails, return to login page with an error
                ViewBag.Error = "Email or password is wrong";
                return View();
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
        public IActionResult ForgotPassword(ForgotPassword model)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == model.Email);
            this.Email = model.Email;
            if (customer != null)
            {
                model.ResetCode = Guid.NewGuid().ToString();
                SendEmail(model.Email, "Reset Password", "Reset code: " + model.ResetCode);
                TempData["Email"] = model.Email;
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

        public IActionResult ResetPassword(string Email)
        {
            var forgotPassword = new ForgotPassword();
            forgotPassword.Email = TempData["Email"] as string;
            return View(forgotPassword);
        }
        [HttpPost]
        public IActionResult ResetPassword(ForgotPassword forgotPassword)
        {
            var customer = _db.Customers.FirstOrDefault(c => c.Email == forgotPassword.Email);
            if (customer != null)
            {
                if (customer.ResetCode == forgotPassword.ResetCode) // Thay bằng cách kiểm tra reset code hợp lệ
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
        //staff login
        public IActionResult StaffLogin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult StaffLogin(string email, string password)
        {
            var staff = _db.Staff.FirstOrDefault(c => c.Email == email && c.Password == password);

            if (staff != null)
            {
                // Successful login, redirect to dashboard
                HttpContext.Session.SetObject("LoggedInStaff", staff);
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                // If login fails, return to login page with an error
                ViewBag.Error = "Email or password is wrong";
                return View();
            }
        }
    }
}
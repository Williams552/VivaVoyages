using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
    public class CustomerController : Controller
    {
        private readonly VivaVoyagesContext _db;
        public CustomerController(VivaVoyagesContext db)
        {
            _db = db;
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
        public IActionResult CheckUsernameUniqueness(string Email, int CustomerId)
        {
            var isUnique = !_db.Customers.Any(c => c.Email == Email && c.CustomerId != CustomerId);
            return Json(isUnique);
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
            if (email != null) return RedirectToAction("ForgotPassword");
            if (_db.Customers.Any(c => c.Email == email))
            {
                string resetCode = Guid.NewGuid().ToString();
                SendEmail(email, resetCode);
            }
            else
            {
                ModelState.AddModelError("Email", "Email not found");
                return View();
            }
            return View();
        }
        private void SendEmail(string email, string resetCode)
        {
            try
            {
                // Thông tin của tài khoản email gửi
                var senderEmail = "tiendat552sc@gmail.com";
                var senderPassword = "tiendat2003";

                // Tạo đối tượng SmtpClient
                var smtpClient = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential(senderEmail, senderPassword),
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                // Tạo đối tượng MailMessage
                var mailMessage = new MailMessage
                {
                    From = new MailAddress(senderEmail),
                    Subject = "Subject của email",
                    Body = $"Reset Code: {resetCode}",
                    IsBodyHtml = false,
                };

                // Thêm địa chỉ email nhận
                mailMessage.To.Add(email);

                // Gửi email
                smtpClient.Send(mailMessage);
            }
            catch (System.Exception)
            {
                // Xử lý ngoại lệ khi gửi email thất bại
            }
        }
        public IActionResult ResetPassword()
        {
            // TODO: Your code here
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string email, string resetCode, string newPassword)
        {
            if (email != null && resetCode != null && newPassword != null)
            {
                var customer = _db.Customers.FirstOrDefault(c => c.Email == email);
                if (customer != null)
                {
                    if (resetCode == "123456")
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
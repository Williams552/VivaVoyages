using Microsoft.AspNetCore.Mvc;
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
                // email exists
                // You can return a JSON object with a property to indicate the email exists
                // For example: return Json(new { emailExists = true });
                return Json(new { emailExists = true });
            }
            else
            {
                // email does not exist
                // You can return a JSON object with a property to indicate the email does not exist
                // For example: return Json(new { emailExists = false });
                return Json(new { emailExists = false });
            }
        }
    }
}
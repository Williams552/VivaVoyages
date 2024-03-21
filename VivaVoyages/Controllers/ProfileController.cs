using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VivaVoyages.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using VivaVoyages.Filters;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace VivaVoyages.Controllers
{
    [ServiceFilter(typeof(CustomerLoginFilter))]
    public class ProfileController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public ProfileController(VivaVoyagesContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var customer = HttpContext.Session.GetObject<Customer>("LoggedInCustomer");
            return View(customer);
        }

        public IActionResult Edit(int id)
        {
            var customer = this._context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        [HttpPost]
        public IActionResult Edit(Customer customer)
        {

            if (string.IsNullOrEmpty(customer.FullName))
            {
                ModelState.AddModelError("fullName", "Full Name is required");
                // Trả về view với model để người dùng có thể nhập lại thông tin
                return View(customer);
            }
            if (string.IsNullOrEmpty(customer.PhoneNumber))
            {
                ModelState.AddModelError("phoneNumber", "Phone number is required");
                return View(customer);
            }
            // Sử dụng regex để kiểm tra định dạng số điện thoại
            else if (!System.Text.RegularExpressions.Regex.IsMatch(customer.PhoneNumber, @"^0[0-9]{9}$"))
            {
                ModelState.AddModelError("phoneNumber", "Phone number must be 10 digits long and start with 0.");
                return View(customer);
            }
            if (string.IsNullOrEmpty(customer.Address))
            {
                ModelState.AddModelError("address", "Address is required");
                // Trả về view với model để người dùng có thể nhập lại thông tin
                return View(customer);
            }
            if (string.IsNullOrEmpty(customer.Email))
            {
                ModelState.AddModelError("email", "Email is required");
                // Trả về view với model để người dùng có thể nhập lại thông tin
                return View(customer);
            }
            DateOnly today = DateOnly.FromDateTime(DateTime.Today);
            if (customer.Dob == DateOnly.MinValue)
            {
                ModelState.AddModelError("dob", "Birthday is required.");
                return View(customer);
            }
            else if (customer.Dob >= today)
            {
                ModelState.AddModelError("dob", "Birthday must be a date in the past.");
                return View(customer);
            }
            customer.Status = "Active";
            _context.Customers.Update(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");

            return View();


        }

        // GET: Profile/ChangePassword/5
        public IActionResult ChangePassword(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // POST: Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(int customerID, string oldPassword, string newPassword, string confirmPassword)
        {
            try
            {
                var customer = _context.Customers.Find(customerID);
                if (customer == null)
                {
                    return NotFound();
                }

                // Kiểm tra mật khẩu cũ
                oldPassword = HashPassword(oldPassword);
                if (customer.Password != oldPassword)
                {
                    ModelState.AddModelError(string.Empty, "Old password is incorrect.");
                    return View(customer); // Đảm bảo truyền đối tượng customer để giữ id và các thông tin khác
                }

                if (newPassword != confirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "New Password and Confirm Password do not match.");
                    return View(customer);
                }

                TempData["ChangePasswordSuccess"] = "Password changed successfully.";

                newPassword = HashPassword(newPassword);
                customer.Password = newPassword;
                _context.SaveChanges();
                ViewData["SuccessMessage"] = "Password changed successfully.";


                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi (ex) để phân tích
                return StatusCode(500, "Internal server error");
            }
        }



        public async Task<IActionResult> GetCustomerTours(int id)
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.CustomerId == id);
            if (customer == null)
            {
                // Khách hàng không tồn tại
                return NotFound($"Customer with ID {id} not found.");
            }
            ViewData["CustomerId"] = customer.CustomerId;
            ViewData["FullName"] = customer.FullName;

            var customerTours = await _context.Orders
            .Where(o => o.CustomerId == id)
            .Include(o => o.Tour)
            .Include(o => o.Passengers) // Đảm bảo include thông tin về Passenger
            .GroupBy(o => o.TourId)
            .Select(g => new CustomerTourView
            {
                OrderId = g.First().OrderId,
                TourName = g.First().Tour.TourName,
                PassengerCount = g.SelectMany(o => o.Passengers).Count(), // Tính tổng số hành khách cho mỗi Tour
                DateStart = g.First().Tour.DateStart.ToString("dd/MM/yyyy"),
                TourDates = g.First().Tour.TourDates,
                TourGuide = g.First().Tour.TourGuide,
                Total = g.First().Total,
                Status = g.First().Status
                // Các thuộc tính khác như trước
            })
            .ToListAsync();

            if (!customerTours.Any())
            {
                // Không tìm thấy tours cho khách hàng này
                return NotFound("No tours found for this customer.");
            }

            // Trả về view với danh sách các tours
            return View(customerTours);
        }

        public IActionResult SignOut()
        {
            HttpContext.Session.Remove("LoggedInCustomer");
            return RedirectToAction("Index", "Home");
        }
        //PBKDF2 Hashing
        public string HashPassword(string password)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: new byte[128 / 8],
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

    }
}


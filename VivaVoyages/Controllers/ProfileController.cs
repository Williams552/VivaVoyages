using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using VivaVoyages.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace VivaVoyages.Controllers
{
    public class ProfileController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public ProfileController(VivaVoyagesContext context)
        {
            this._context = context;
        }

        public IActionResult Index(int? id = 1)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return NotFound();
            }

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
                // Trả về view với model để người dùng có thể nhập lại thông tin
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

            // Chú ý: Sử dụng model Customer có thể tiết lộ thông tin không mong muốn.
            // Trong ví dụ này, chúng ta sẽ chỉ sử dụng customerId và password fields.
            return View(customer);
        }

        // POST: Profile/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(int customerID, string password, string confirmPassword)
        {
            try
            {
                if (password != confirmPassword)
                {
                    ModelState.AddModelError(string.Empty, "Password and Confirm Password do not match.");
                    return View();
                }

                var customer = _context.Customers.Find(customerID);
                if (customer == null)
                {
                    return NotFound();
                }

                // Cập nhật mật khẩu - trong thực tế, bạn nên băm mật khẩu này
                customer.Password = password;
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Log lỗi (ex) để phân tích
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult FavoriteTour(int id)
        {
            return View();
        }



        public async Task<IActionResult> GetCustomerTours(int customerId)
        {
            // Lấy thông tin các tour mà khách hàng đã đặt
            var customerTours = await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Tour)
                .Select(o => new
                {
                    o.Tour.TourName,
                    o.Tour.MaxPasseger,
                    DateStart = o.Tour.DateStart.ToString("dd/MM/yyyy"),
                    o.Tour.TourDates,
                    o.Tour.TourGuide,
                    o.Tour.Cost
                })
                .ToListAsync();

            if (customerTours == null || customerTours.Count == 0)
            {
                return NotFound("No tours found for this customer.");
            }

            // Lưu ý: Bạn cần tạo một ViewModel hoặc sử dụng dynamic để truyền dữ liệu vào View
            // Đây chỉ là ví dụ trả về dữ liệu dưới dạng JSON
            return Json(customerTours);
        }

    }
}


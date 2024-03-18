using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
    
    public class PaymentController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public PaymentController(VivaVoyagesContext context)
        {
            _context = context;
        }

        // GET: Payment/Index
        [HttpGet]
        public IActionResult Index(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            // Truyền order vào view để hiển thị thông tin cần thanh toán
            return View(order);
        }
        
        // POST: Payment/ProcessPayment
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                // Giả lập quá trình thanh toán thành công
                order.Status = "Paid";
                _context.SaveChanges();

                // Chuyển hướng người dùng đến một trang hoặc hành động xác nhận thanh toán thành công
                return RedirectToAction("PaymentSuccess", new { orderId = orderId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                // Hiển thị thông báo lỗi hoặc trang lỗi
                return RedirectToAction("Error", "Home");
            }
        }

        // GET: Payment/PaymentSuccess
        public IActionResult PaymentSuccess(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
        
    }
}
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

        [HttpGet]
        public IActionResult Index(int orderId)
        {
            if (orderId <= 0)
            {
                return NotFound();
            }

            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ProcessPayment(int orderId)
        {
            if (orderId <= 0)
            {
                return NotFound();
            }

            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            try
            {
                order.Status = "Paid";
                _context.SaveChanges();

                return RedirectToAction("PaymentSuccess",new { orderId = orderId });
            }
            catch (Exception ex)
            {
                // Ví dụ về log lỗi, bạn cần cài đặt logger thực sự trong ứng dụng của mình
                Console.Error.WriteLine($"An error occurred while processing payment: {ex.Message}");
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult PaymentSuccess(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}
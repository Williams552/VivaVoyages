using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;
using VivaVoyages.Filters;

namespace VivaVoyages.Controllers
{
    [ServiceFilter(typeof(StaffLoginFilter))]
    public class StaffController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public StaffController(VivaVoyagesContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> TourIndex()
        {
            return View(await _context.Tours.ToListAsync());
        }


        public async Task<IActionResult> TourCancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(d => d.Destinations) //add them de view dc cai table trong view
                .Include(d => d.Orders)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);
        }


        // Action to cancel all orders for a specific tour
        [HttpPost, ActionName("TourCancel")]
        public async Task<IActionResult> TourCancelConfirmed(int tourId)
        {
            // Retrieve all orders for the given tour
            var orders = _context.Orders
                .Include(o => o.Tour)
                .Where(o => o.TourId == tourId)
                .ToList();

            // Change the status of each order to "cancel"
            foreach (var order in orders)
            {
                order.Status = "Cancel";
            }

            // Save changes to the database
            _context.SaveChanges();

            return RedirectToAction("TourIndex", "Staff"); // Redirect to a suitable page
        }

        public async Task<IActionResult> DetailTourOrder(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var tour = await _context.Tours
                .Include(d => d.Destinations) //add them de view dc cai table trong view
                .Include(d => d.Orders)
                .FirstOrDefaultAsync(m => m.TourId == id);
            if (tour == null)
            {
                return NotFound();
            }

            return View(tour);

        }

        public async Task<IActionResult> PendingOrders()
        {
            var pendingOrders = _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Tour)
                .Where(o => o.Status == "Pending")
                .ToList();

            return View(pendingOrders);
        }

        [HttpPost]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, string newStatus)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);

            if (order == null)
            {
                return NotFound();
            }

            // Update the order status
            order.Status = newStatus;

            // Save changes to the database
            _context.SaveChanges();

            return RedirectToAction("PendingOrders", "Staff"); // Redirect to a suitable page
        }


    }
}
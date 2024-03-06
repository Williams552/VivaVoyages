using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
    public class OrderController : Controller
    {
        private readonly VivaVoyagesContext _context;
        private List<Customer> customers;
        private List<Staff> staff;
        private List<Tour> tours;

        public OrderController(VivaVoyagesContext context)
        {
            _context = context;
            customers = _context.Customers.ToList();
            staff = _context.Staff.ToList();
            tours = _context.Tours.ToList();
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _context.Orders.Include(o => o.Customer)
                                            .Include(o => o.Staff)
                                            .Include(o => o.Tour)
                                            .ToListAsync();

            return View(orders);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            ViewData["TourId"] = tours;

            return View(order);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,StaffId,TourId,Status,DateCreated")] Order order)
        {
            _context.Update(order);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.Tour)
                .FirstOrDefaultAsync(m => m.OrderId == id);

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Create()
        {
            ViewData["CustomerId"] = customers;
            ViewData["StaffId"] = staff;
            ViewData["TourId"] = tours;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CustomerId,StaffId,TourId,Status,DateCreated")] Order order)
        {
            order.Tour = _context.Tours.Find(order.TourId);
            order.Customer = _context.Customers.Find(order.CustomerId);
            order.Staff = _context.Staff.Find(order.StaffId);
            order.DateCreated = DateTime.Now;
            order.Status = "Pending";
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is not valid, return to the view with the order model
            return View(order);
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderId == id);
        }

    }
}
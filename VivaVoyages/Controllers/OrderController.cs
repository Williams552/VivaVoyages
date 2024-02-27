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

            public OrderController(VivaVoyagesContext context)
            {
                _context = context;
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

                return View(order);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(int id, [Bind("OrderId,CustomerId,StaffId,TourId,Status,DateCreated")] Order order)
            {
                if (id != order.OrderId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(order);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!_context.Orders.Any(e => e.OrderId == order.OrderId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
                return View(order);
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
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name");
                ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Name");
                ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Name");
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create([Bind("CustomerId,StaffId,TourId,Status,DateCreated")] Order order)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "Name", order.CustomerId);
                ViewData["StaffId"] = new SelectList(_context.Staff, "StaffId", "Name", order.StaffId);
                ViewData["TourId"] = new SelectList(_context.Tours, "TourId", "Name", order.TourId);
                return View(order);
            }
        }
    }
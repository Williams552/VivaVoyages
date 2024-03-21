using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VivaVoyages.Filters;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers
{
    [ServiceFilter(typeof(StaffLoginFilter))]

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
            order.Status = "Cancelled";
            _context.Update(order);
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
        public async Task<IActionResult> Create(int tourId, int customerId, string passengersJson, string CouponCode)
        {
            try
            {
                //check passengersJson
                if (passengersJson == "[]")
                {
                    TempData["Error"] = "Enter infomation of passengers";
                    return RedirectToAction("Create", new { id = tourId });
                }

                Staff staff = HttpContext.Session.GetObject<Staff>("LoggedInStaff");
                int numbOfPassenger = 0;
                int numbOfSR = 0;

                var tour = _context.Tours.Find(tourId);

                Order order = new Order
                {
                    CustomerId = customerId,
                    TourId = tourId,
                    //Find the staff has less orders pending to assign the order to
                    StaffId = staff.StaffId,
                    DateCreated = DateTime.Now,
                    Status = "Pending",
                };

                // Add the Order to the context
                _context.Orders.Add(order);

                // Save changes to the database
                _context.SaveChanges();


                //Add the passengers to the database
                List<Passenger> passengers = JsonConvert.DeserializeObject<List<Passenger>>(passengersJson);
                foreach (var passenger in passengers)
                {
                    numbOfPassenger++;
                    if (passenger.SingleRoom == true) numbOfSR++;
                    passenger.CustomerId = customerId;
                    passenger.OrderId = order.OrderId;
                    _context.Passengers.Add(passenger);
                    _context.SaveChanges();
                }
                order.Total = ((tour.Cost + tour.ExpectedProfit) * numbOfPassenger + (tour.SingleRoomCost * numbOfSR)) * (1 + tour.Tax / 100);
                var coupon = _context.Coupons.Find(CouponCode);
                if (coupon != null)
                {
                    if (coupon.DateEnd < DateOnly.FromDateTime(DateTime.Now))
                    {
                        TempData["Error"] = "Coupon is expired";
                        return RedirectToAction("Create", new { id = tourId });
                    }
                    order.CouponCode = CouponCode;
                    order.Total = order.Total - coupon.Discount;
                }

                _context.Orders.Update(order);
                _context.SaveChanges();

                return RedirectToAction("Index", "Order", new { orderId = order.OrderId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return RedirectToAction("Error", "Home");
            }
        }
    }
}
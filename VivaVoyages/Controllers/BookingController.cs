using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Framework;
using Microsoft.DotNet.Scaffolding.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using VivaVoyages.Models;


namespace VivaVoyages.Controllers
{
    public class BookingController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public BookingController(VivaVoyagesContext context)
        {
            _context = context;
        }

        // GET: Booking/Create
        [HttpGet]
        public IActionResult Create(int id)
        {
            var order = new Order();
            order.TourId = id;
            order.Tour = _context.Tours.Include(t => t.Destinations).FirstOrDefault(t => t.TourId == id);
            order.Tour.Destinations = _context.Destinations.Include(d => d.Place).Where(d => d.TourId == id).ToList();

            order.Customer = HttpContext.Session.GetObject<Customer>("LoggedInCustomer");
            return View(order);
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int tourId, string passengersJson)
        {
            try
            {
                //check passengersJson
                if (passengersJson == "[]")
                {
                    ViewData["Error"] = "Enter infomation of passengers";
                    return RedirectToAction("Create", new { id = tourId });
                }

                Customer customer = HttpContext.Session.GetObject<Customer>("LoggedInCustomer");
                int numbOfPassenger = 0;
                int numbOfSR = 0;

                var tour = _context.Tours.Find(tourId);

                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    TourId = tourId,
                    //Find the staff has less orders pending to assign the order to
                    StaffId = _context.Staff.OrderBy(s => s.Orders.Count(o => o.Status == "Pending")).FirstOrDefault().StaffId,
                    DateCreated = DateTime.Now,
                    Status = "Pending",
                    Total = ((tour.Cost + tour.ExpectedProfit) * numbOfPassenger + (tour.SingleRoomCost * numbOfSR)) * (1 + tour.Tax / 100)
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
                    passenger.CustomerId = customer.CustomerId;
                    passenger.OrderId = order.OrderId;
                    _context.Passengers.Add(passenger);
                    _context.SaveChanges();
                }


                return RedirectToAction("Index", "Payment", new { orderId = order.OrderId });
            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return RedirectToAction("Error", "Home");
            }
        }

        // POST: Booking/Save
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(List<Passenger> passengers)
        {
            if (ModelState.IsValid)
            {
                // Save passengers to the database
                foreach (var passenger in passengers)
                {
                    _context.Passengers.Add(passenger);
                }
                _context.SaveChanges();

                // Redirect to list of passengers
                return RedirectToAction(nameof(ListPassengers));
            }
            return View(passengers);
        }

        // GET: Booking/ListPassengers
        public IActionResult ListPassengers()
        {
            var passengerList = _context.Passengers.ToList();
            return View(passengerList);
        }

        public async Task<IActionResult> OrderCancel(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Tour)
                .Include(o => o.Customer)
                .Include(o => o.Passengers)
                .Include(o => o.Staff)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // Action to cancel all orders for a specific tour
        [HttpPost, ActionName("OrderCancel")]
        public async Task<IActionResult> OrderCancelConfirmed(int id)
        {
            try
            {
                // Find the order
                var order = await _context.Orders.FindAsync(id);

                if (order == null)
                {
                    // Order not found, handle accordingly
                    return RedirectToAction("OrderNotFound", "Error");
                }

                order.Status = "Cancelled";
                _context.Update(order);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
            return RedirectToAction("Index", "Profile");
        }

    }
}

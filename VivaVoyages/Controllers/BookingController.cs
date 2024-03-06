using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Create(int tourId, int customerId, string passengersJson)
        {
            try
            {
                List<Passenger> passengers = JsonConvert.DeserializeObject<List<Passenger>>(passengersJson);
                Customer customer = HttpContext.Session.GetObject<Customer>("LoggedInCustomer");
                customer.CustomerId = 0;
                Order order = new Order
                {
                    CustomerId = customerId,
                    Customer = customer,
                    TourId = tourId,
                    StaffId = _context.Staff.FirstOrDefault().StaffId,
                    DateCreated = DateTime.Now,
                    Status = "Pending"
                };


                // Set the Passengers for the Customer
                order.Customer.Passengers = passengers;

                // Add the Order to the context
                _context.Orders.Add(order);

                // Save changes to the database
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
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

    }
}
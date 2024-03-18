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
        public IActionResult Create(int tourId, string passengersJson)
        {
            try
            {
                Customer customer = HttpContext.Session.GetObject<Customer>("LoggedInCustomer");
                int numbOfPassenger =0;
                int numbOfSR =0;

                //Add the passengers to the database
                List<Passenger> passengers = JsonConvert.DeserializeObject<List<Passenger>>(passengersJson);
                foreach (var passenger in passengers)
                {
                    numbOfPassenger ++;
                    if(passenger.SingleRoom == true) numbOfSR ++;
                    passenger.CustomerId = customer.CustomerId;
                    _context.Passengers.Add(passenger);
                    
                }
                var tour =  _context.Tours.Find(tourId);

                Order order = new Order
                {
                    CustomerId = customer.CustomerId,
                    TourId = tourId,
                    StaffId = _context.Staff.FirstOrDefault().StaffId,
                    DateCreated = DateTime.Now,
                    Status = "Pending",
                    Total = ((tour.Cost + tour.ExpectedProfit) * numbOfPassenger + (tour.SingleRoomCost * numbOfSR))*(1+tour.Tax/100)

                };

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
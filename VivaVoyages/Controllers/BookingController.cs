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
    public class BookingController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public BookingController(VivaVoyagesContext context)
        {
            _context = context;
        }

        // GET: Booking/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Booking/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(int numberOfPassengers)
        {
            List<Passenger> passengers = new List<Passenger>();

            for (int i = 0; i < numberOfPassengers; i++)
            {
                passengers.Add(new Passenger());
            }

            ViewBag.NumberOfPassengers = numberOfPassengers;
            return View(passengers);
        }

        // POST: Booking/Save
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
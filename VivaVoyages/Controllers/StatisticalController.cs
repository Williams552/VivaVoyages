using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;
using Microsoft.EntityFrameworkCore;

namespace VivaVoyages.Controllers
{
    public class StatisticalController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public StatisticalController(VivaVoyagesContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {
            var tourWithMostPassengers = _context.Orders
              .Include(o => o.Passengers)
              .Include(o => o.Tour)
              .AsEnumerable() // Sử dụng AsEnumerable để chuyển sang LINQ to Objects nếu cần
              .GroupBy(o => o.TourId)
              .Select(group => new Statistical
              {
                  Tour = group.First().Tour,
                  PassengerCount = group.Sum(o => o.Passengers.Count)
              })
              .OrderByDescending(t => t.PassengerCount)
              .ToList();

            return View(tourWithMostPassengers);
        }
    }
}
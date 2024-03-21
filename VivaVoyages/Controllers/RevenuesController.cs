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
    public class RevenuesController : Controller
    {
        private readonly VivaVoyagesContext _context;

        public RevenuesController(VivaVoyagesContext context)
        {
            _context = context;
        }

        public IActionResult RevenueStatistics()
        {
            var tours = _context.Tours
                .Include(t => t.Orders)
                .ThenInclude(o => o.Passengers)
                .ToList(); // Lấy toàn bộ dữ liệu cần thiết

            var toursWithTotalProfit = tours.Select(t => new Revenues
            {
                TourId = t.TourId,
                TourName = t.TourName,
                TotalPassengers = t.Orders.Sum(o => o.Passengers.Count), // Tính tổng số lượng hành khách
                ProfitPreTour = t.ExpectedProfit,
                TotalProfit = t.Orders.Sum(o => o.Passengers.Count) * t.ExpectedProfit // Nhân với ExpectedProfit
            })
            .ToList();

            return View(toursWithTotalProfit);
        }

    }
}
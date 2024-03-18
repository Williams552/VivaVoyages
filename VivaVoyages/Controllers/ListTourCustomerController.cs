using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;



namespace VivaVoyages.Controllers
{

    public class ListTourCustomerController : Controller
    {
        private readonly VivaVoyagesContext _db;
        public ListTourCustomerController(VivaVoyagesContext db)
        {
            _db = db;
        }
        public IActionResult ListTours()
        {
            IEnumerable<Tour> listem = _db.Tours.ToList();
            return View(listem);
        }

        public IActionResult ViewTourDetails(int id)
        {

            var tour = _db.Tours.Include(t => t.Destinations).FirstOrDefault(t => t.TourId == id);
            tour.Destinations = _db.Destinations.Include(d => d.Place).Where(d => d.TourId == id).ToList();
            var otherTours = _db.Tours.Where(t => t.TourId != id).Take(4).ToList();

    if (tour != null)
    {
        ViewBag.OtherTours = otherTours; // Chuyển danh sách tour khác vào view
        return View(tour);
    }
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ListToursSearch()
        {
            IEnumerable<Tour> listem = _db.Tours.ToList();
            return View(listem);
        }

        public IActionResult SearchTours(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var allTours = _db.Tours.ToList();
                return View("ListTours", allTours);
            }

            var searchTermLower = searchTerm.ToLower();
            var foundTours = _db.Tours.Where(t => t.TourName.ToLower().Contains(searchTermLower)).ToList();

            // Kiểm tra nếu không có tour nào được tìm thấy
            if (foundTours.Count == 0)
            {
                return RedirectToAction("notfound");
            }

            return View("ListToursSearch", foundTours);
        }


        public IActionResult notfound()
        {
            // TODO: Your code here
            return View();
        }

     
       

    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using VivaVoyages.Models;
using PagedList;


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
    if (tour != null)
    {
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
            return View("ListTours", foundTours);
        }







    }
}
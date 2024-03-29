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
            var tour = _db.Tours
                           .Include(t => t.Destinations)
                           .Include(t => t.CouponCodeNavigation)
                           .FirstOrDefault(t => t.TourId == id);

            tour.Destinations = _db.Destinations.Include(d => d.Place).Where(d => d.TourId == id).ToList();

            var otherTours = _db.Tours.Where(t => t.TourId != id).Take(4).ToList();

            if (tour != null)
            {
                // Kiểm tra và ẩn hình ảnh nếu ImagePath của Place là null
                foreach (var destination in tour.Destinations)
                {
                    if (destination.Place != null && string.IsNullOrEmpty(destination.Place.ImagePath))
                    {
                        destination.Place.ImagePath = null;
                    }
                }

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
                // Trả về view "ListTours" khi không có điều kiện tìm kiếm được cung cấp
                return RedirectToAction("ListTours");
            }

           var searchTermLower = searchTerm.ToLower();
            var foundTours = _db.Tours.Where(t => t.TourName.ToLower().Contains(searchTermLower)).Include(t => t.CouponCodeNavigation) // Bao gồm thông tin từ Coupon
                                    .ToList();

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

        
        public IActionResult ListToursWithCoupon()
        {
            var toursWithCoupon = _db.Tours
                                    .Where(t => !string.IsNullOrEmpty(t.CouponCode))
                                    .Include(t => t.CouponCodeNavigation) // Bao gồm thông tin từ Coupon
                                    .ToList();
            return View(toursWithCoupon);
        }

        public IActionResult PromotionEvent()
        {
         DateOnly currentDate = DateOnly.FromDateTime(DateTimeOffset.Now.Date);
            var activeCoupons  = _db.Coupons
                                    .Where(c => c.DateStart <= currentDate && c.DateEnd >= currentDate )
                                    .ToList();
                                    
            return View(activeCoupons);
        }


    }
}
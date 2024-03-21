using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly VivaVoyagesContext _context;

    public HomeController(ILogger<HomeController> logger, VivaVoyagesContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        var tour = _context.Tours.Include(d => d.Destinations).ToList();

        if (tour.Count < 3)
        {
            for (int i = 0; i < 3 - tour.Count; i++)
            {
                tour.Add(new Tour { Destinations = new List<Destination> { new Destination() } });
            }
        }

        return View(tour);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public IActionResult Profile()
    {
        // Kiểm tra xem người dùng đã đăng nhập chưa
        if (!User.Identity.IsAuthenticated)
        {
            // Chưa đăng nhập, chuyển hướng đến trang đăng nhập
            return RedirectToAction("Login", "Customer");
        }
        else
        {
            // Đã đăng nhập, chuyển hướng đến trang Profile/Index
            return RedirectToAction("Index", "Profile");
        }
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public IActionResult handbook()
    {
        // TODO: Your code here
        return View();
    }

}


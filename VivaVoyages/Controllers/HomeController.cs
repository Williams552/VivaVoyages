using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VivaVoyages.Models;

namespace VivaVoyages.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
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


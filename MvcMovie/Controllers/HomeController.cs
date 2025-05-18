using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcMovie.Models;

namespace MvcMovie.Controllers;

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
        //return Redirect("/Identity/Account/Login");
        
    }
    [HttpPost]
    public IActionResult Index(string fullName, string Address)
    {
        string strOuput =" xin chao 1 "+ fullName+" dến từ " + Address;
        ViewBag.Message = strOuput;
        return View();
        
    }
    


    public IActionResult Privacy()
    {
        return View();
        //return Redirect("/Identity/Account/Login");
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

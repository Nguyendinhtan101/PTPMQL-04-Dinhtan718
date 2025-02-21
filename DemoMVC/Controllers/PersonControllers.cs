using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FirstWebMVC.Models;
using DemoMVC.Models.Entities;

namespace FirstWebMVC.Controllers;

public class PersonController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Index(Person ps)
    {
        string strOutput = "Xin chào" + " - " + ps.PersonID + " - " + ps.HoTen + " - " + ps.QueQuan;
        ViewBag.infoPerson = strOutput;
        return View();
    }
}
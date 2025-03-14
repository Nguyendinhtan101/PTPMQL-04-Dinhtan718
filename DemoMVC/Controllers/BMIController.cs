
using Microsoft.AspNetCore.Mvc;
using BAITAP2102.Models;

namespace BAITAP2102.Controllers
{
    public class BMIController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(BMIModel model)
        {
            model.CalculateBMI();
            ViewBag.Message = $"BMI: {model.BMI:F2} - {model.Result}";
            return View(model);
        }
    }
}

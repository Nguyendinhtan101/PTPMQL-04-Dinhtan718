
using BAITAP2102.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAITAP2102.Controllers
{
    public class GradeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(GradeModel model)
        {
            if (ModelState.IsValid)
            {
                double finalScore = model.CalculateFinalScore();
                ViewBag.Result = $"Điểm tổng kết của bạn là: {finalScore:F2}";
            }
            else
            {
                ViewBag.Result = "Vui lòng nhập đầy đủ và đúng dữ liệu!";
            }

            return View("Index", model);
        }
    }
}

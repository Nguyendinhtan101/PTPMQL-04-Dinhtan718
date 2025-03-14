
using BAITAP2102.Models;
using Microsoft.AspNetCore.Mvc;

namespace BAITAP2102.Controllers
{
    public class InvoiceController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(InvoiceModel model)
        {
            if (ModelState.IsValid)
            {
                double total = model.CalculateTotal();
                ViewBag.Result = $"Tổng tiền hóa đơn: {total:N0} VNĐ";
            }
            else
            {
                ViewBag.Result = "Vui lòng nhập đúng dữ liệu!";
            }

            return View("Index", model);
        }
    }
}


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Thêm namespace này
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.Process;
using OfficeOpenXml;

namespace MvcMovie.Controllers
{
    public class PersonController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ExcelProcess _excelProcess = new();
        private readonly ILogger<PersonController> _logger; // Thêm logger
        private string fileLocation = string.Empty;

        public PersonController(ApplicationDbContext context, ILogger<PersonController> logger)
        {
            _context = context;
            _logger = logger; // Inject logger
        }

        // GET: Person
        public async Task<IActionResult> Index()
        {
            return View(await _context.Persons.ToListAsync());
        }

        // GET: Person/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);
            return person == null ? NotFound() : View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PersonId,FullName,Address")] Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(person);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var person = await _context.Persons.FindAsync(id);
            return person == null ? NotFound() : View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,FullName,Address")] Person person)
        {
            if (id != person.PersonId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(person);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.PersonId))
                    {
                        return NotFound();
                    }
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return NotFound();
            }

            var person = await _context.Persons.FirstOrDefaultAsync(m => m.PersonId == id);
            return person == null ? NotFound() : View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var person = await _context.Persons.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            _context.Persons.Remove(person);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(string id)
        {
            return _context.Persons.Any(e => e.PersonId == id);
        }
        public IActionResult Download()
        {
            // Name the file when downloading
            var fileName = "YourFileName" + ".xlsx";
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                // add some text to cell A1
                worksheet.Cells["A1"].Value = "PersonID";
                worksheet.Cells["B1"].Value = "FullName";
                worksheet.Cells["C1"].Value = "Address";
                // get all Person
                var personList = _context.Persons.ToList();
                // fill data to worksheet
                worksheet.Cells["A2"].LoadFromCollection(personList);
                var stream = new MemoryStream(excelPackage.GetAsByteArray());
                // download file
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
        }

     // POST: Person/Upload
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            _logger.LogInformation("📂 Bắt đầu Upload file...");

            if (file == null || file.Length == 0)
            {
                _logger.LogWarning("⚠️ Không có file nào được chọn để tải lên.");
                ModelState.AddModelError("", "Please choose an Excel file to upload!");
                return View();
            }

            string fileExtension = Path.GetExtension(file.FileName);
            _logger.LogInformation($"🔍 Kiểm tra loại file: {fileExtension}");

            if (fileExtension != ".xls" && fileExtension != ".xlsx")
            {
                _logger.LogWarning("⚠️ Loại file không hợp lệ!");
                ModelState.AddModelError("", "Invalid file type. Please upload an Excel file!");
                return View();
            }

            var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExtension;
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Uploads/Excels", fileName);
            _logger.LogInformation($"📌 Đường dẫn lưu file: {filePath}");

            try
            {
                _logger.LogInformation("💾 Đang lưu file vào server...");
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                fileLocation = filePath;
                var dt = _excelProcess.ExcelToDataTable(fileLocation) as System.Data.DataTable;

                if (dt == null || dt.Rows.Count == 0)
                {
                    _logger.LogWarning("⚠️ Không có dữ liệu nào trong file Excel!");
                    ModelState.AddModelError("", "No data found in the uploaded file!");
                    return View();
                }

                _logger.LogInformation($"✅ File hợp lệ, đọc {dt.Rows.Count} dòng dữ liệu.");

                foreach (System.Data.DataRow row in dt.Rows)
                {
                    _logger.LogInformation($"📝 Dữ liệu: PersonId={row[0]?.ToString()}, FullName={row[1]?.ToString()}, Address={row[2]?.ToString()}");
                    
                    var ps = new Person
                    {
                        PersonId = row[0]?.ToString() ?? string.Empty,
                        FullName = row[1]?.ToString() ?? string.Empty,
                        Address = row[2]?.ToString() ?? string.Empty
                    };
                    _context.Add(ps);
                }

                await _context.SaveChangesAsync();
                _logger.LogInformation("✅ Lưu dữ liệu thành công!");

                ViewBag.Message = "File uploaded and data saved successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"❌ Lỗi khi xử lý file: {ex.Message}");
                ModelState.AddModelError("", "Error processing file: " + ex.Message);
                return View();
            }
        }
    
    }
}

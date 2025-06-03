using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
using MvcMovie.Models.Process;
using X.PagedList;
using OfficeOpenXml;
using Microsoft.AspNetCore.Mvc.Rendering;
//using X.PagedList.Async;


namespace MvcMovie.Controllers
{
    public class PersonController : Controller

    { 
        private readonly ApplicationDbContext _context;
        private  ExcelProcess _excelProcess = new ExcelProcess();


        public PersonController(ApplicationDbContext context)
        {
            _context = context;

        }



 public async Task<IActionResult> Index(int? page, int? PageSize)
{
    // Tạo danh sách lựa chọn số lượng bản ghi mỗi trang
    ViewBag.PageSize = new List<SelectListItem>()
    {
        new SelectListItem() { Value = "3", Text = "3" },
        new SelectListItem() { Value = "5", Text = "5" },
        new SelectListItem() { Value = "10", Text = "10" },
        new SelectListItem() { Value = "15", Text = "15" },
        new SelectListItem() { Value = "25", Text = "25" },
        new SelectListItem() { Value = "50", Text = "50" },
    };

    // Nếu không có PageSize thì mặc định là 5
    int pageSize = PageSize ?? 5;
    int pageNumber = page ?? 1;

    ViewBag.psize = pageSize;

    // Truy vấn có phân trang trực tiếp từ database
    var model = await _context.Person
        .OrderBy(p => p.PersonId)
        .ToPagedListAsync(pageNumber, pageSize);

    return View(model);
}


        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]


      public async Task<IActionResult> Create([Bind("PersonId,Fullname,Address,Gender")]Person person)
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
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person.FindAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
         private bool PersonExists(string id)
         {
             return _context.Person.Any(e => e.PersonId == id);
         }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PersonId,Fullname,Address,Gender")]Person person)
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
                    else
                    {
                        throw;
                    }
                    
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)

        {
            if (id == null || _context.Person == null)
            {
                return NotFound();
            }

            var person = await _context.Person
                .FirstOrDefaultAsync(m => m.PersonId == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Person ==null)
         {  
           return Problem ("Entity set 'ApplicationDbContext.Person' is null.");
         } 
        
            var person = await _context.Person.FindAsync(id);
            if (person != null)
            {
                _context.Person.Remove(person);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        
        }
        
        private bool PersonClassExists(string id)
          {
            return (_context.Person?.Any(e => e.PersonId == id)).GetValueOrDefault();

          
          }
             //POST: Person/UPload
          public async Task<IActionResult> Upload()
          {
            return View();
          }

          [HttpPost]
          [ValidateAntiForgeryToken]
          public async Task<IActionResult> Upload(IFormFile file)
          {
            if (file!=null)

          { 
            string fileExtension = Path.GetExtension(file.FileName);
            if (fileExtension != ".xls" && fileExtension !=".xlsx")
              {
                ModelState.AddModelError("","please choose excel file to Update!");
                
              }
            else
            {
                //rename file when upload to sever
                var fileName = DateTime.Now.ToString("yyyyMMdd_HHmmss") + fileExtension;
                
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "UpLoads","Excels",fileName );
                var fileLocation = new FileInfo(filePath).ToString();
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    //save fil to sever
                    await file.CopyToAsync(stream);
                    //read data form excel file Data
                    var dt = _excelProcess.ExcelToDataTable(fileLocation);
                    //using for loop to read data from dt
                    for (int i = 0;i < dt.Rows.Count; i++ )

                    {
                        var ps = new Person();
                        //set value to attributes
                        ps.PersonId = dt.Rows[i][0].ToString();
                        ps.Fullname = dt.Rows[i][0].ToString();
                        ps.Address = dt.Rows[i][2].ToString();
                        ps.Gender = dt.Rows[i][3].ToString();
                        //add object to context
                        _context.Add(ps);
                    }
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));

                }
            }  

          }
          return View();
             
       }
    
    }
}
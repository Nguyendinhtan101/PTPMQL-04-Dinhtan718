using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;
// using MvcMovie.Moderls.Process 


namespace MvcMovie.Controllers
{
    public class PersonController : Controller

    { 
        private readonly ApplicationDbContext _context;
        //private ExcelProcess _excelProcess = new ExcelProcess();


        public PersonController(ApplicationDbContext context)
        {
            _context = context;

        }
    

        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.ToListAsync();
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
             // POST: Person/UPload
        //   public async Task<IActionResult>Upload()
        //   {
        //     return view();
        //   }
        //   [HttpPost]
        //   [ValidateAntiForgeryToken]
          

       }
    }

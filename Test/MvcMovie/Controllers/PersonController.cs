using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models;


namespace MvcMovie.Controllers
{
    public class PersonController : Controller

    { 
        private readonly ApplicationDbContext _context;


        public PersonController(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<IActionResult> Index()
        {
            var model = await _context.Person.TolistAsync();
            return View(model);
        }
        public IActionResult Create()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]


      public async Task<IActionResult> Create([Bind("Name,FullName,Address")]Person person)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(personClass);
        }

        // GET: Person/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personClass = await _context.People.FindAsync(id);
            if (personClass == null)
            {
                return NotFound();
            }
            return View(personClass);
        }

        // POST: Person/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Name,FullName,Address")]Person person)
        {
            if (id != personClass.Name)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonClassExists(personClass.Name))
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
            return View(personClass);
        }

        // GET: Person/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personClass = await _context.People
                .FirstOrDefaultAsync(m => m.Name == id);
            if (personClass == null)
            {
                return NotFound();
            }

            return View(personClass);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var personClass = await _context.People.FindAsync(id);
            if (personClass != null)
            {
                _context.People.Remove(personClass);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonClassExists(string id)
        {
            return _context.People.Any(e => e.Name == id);
        }
    }
}
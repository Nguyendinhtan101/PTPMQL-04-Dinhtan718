using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Models.Entities;

namespace MvcMovie.Controllers
{
    public class MemberUnitsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MemberUnitsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MemberUnits
        public async Task<IActionResult> Index()
        {
            return View(await _context.MemberUnits.ToListAsync());
        }

        // GET: MemberUnits/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberUnits = await _context.MemberUnits
                .FirstOrDefaultAsync(m => m.MenberUnitId == id);
            if (memberUnits == null)
            {
                return NotFound();
            }

            return View(memberUnits);
        }

        // GET: MemberUnits/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MemberUnits/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MenberUnitId,Name,Address,PhoneNumber,WebsiteUrl")] MemberUnits memberUnits)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memberUnits);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memberUnits);
        }

        // GET: MemberUnits/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberUnits = await _context.MemberUnits.FindAsync(id);
            if (memberUnits == null)
            {
                return NotFound();
            }
            return View(memberUnits);
        }

        // POST: MemberUnits/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MenberUnitId,Name,Address,PhoneNumber,WebsiteUrl")] MemberUnits memberUnits)
        {
            if (id != memberUnits.MenberUnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memberUnits);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberUnitsExists(memberUnits.MenberUnitId))
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
            return View(memberUnits);
        }

        // GET: MemberUnits/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memberUnits = await _context.MemberUnits
                .FirstOrDefaultAsync(m => m.MenberUnitId == id);
            if (memberUnits == null)
            {
                return NotFound();
            }

            return View(memberUnits);
        }

        // POST: MemberUnits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memberUnits = await _context.MemberUnits.FindAsync(id);
            if (memberUnits != null)
            {
                _context.MemberUnits.Remove(memberUnits);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemberUnitsExists(int id)
        {
            return _context.MemberUnits.Any(e => e.MenberUnitId == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VIVs.Models;

namespace VIVs.Controllers
{
    public class VivstestimonialsController : Controller
    {
        private readonly ModelContext _context;

        public VivstestimonialsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivstestimonials
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Vivstestimonials.Include(v => v.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Vivstestimonials/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivstestimonial = await _context.Vivstestimonials
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivstestimonial == null)
            {
                return NotFound();
            }

            return View(vivstestimonial);
        }

        // GET: Vivstestimonials/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid");
            return View();
        }

        // POST: Vivstestimonials/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rating,Opinion,Status,Userid")] Vivstestimonial vivstestimonial)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivstestimonial);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivstestimonial.Userid);
            return View(vivstestimonial);
        }

        // GET: Vivstestimonials/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivstestimonial = await _context.Vivstestimonials.FindAsync(id);
            if (vivstestimonial == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivstestimonial.Userid);
            return View(vivstestimonial);
        }

        // POST: Vivstestimonials/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Rating,Opinion,Status,Userid")] Vivstestimonial vivstestimonial)
        {
            if (id != vivstestimonial.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivstestimonial);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivstestimonialExists(vivstestimonial.Id))
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
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivstestimonial.Userid);
            return View(vivstestimonial);
        }

        // GET: Vivstestimonials/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivstestimonial = await _context.Vivstestimonials
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivstestimonial == null)
            {
                return NotFound();
            }

            return View(vivstestimonial);
        }

        // POST: Vivstestimonials/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivstestimonial = await _context.Vivstestimonials.FindAsync(id);
            _context.Vivstestimonials.Remove(vivstestimonial);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivstestimonialExists(decimal id)
        {
            return _context.Vivstestimonials.Any(e => e.Id == id);
        }
    }
}

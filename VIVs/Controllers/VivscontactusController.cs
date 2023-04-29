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
    public class VivscontactusController : Controller
    {
        private readonly ModelContext _context;

        public VivscontactusController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivscontactus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vivscontactus.ToListAsync());
        }

        // GET: Vivscontactus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivscontactu = await _context.Vivscontactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivscontactu == null)
            {
                return NotFound();
            }

            return View(vivscontactu);
        }

        // GET: Vivscontactus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vivscontactus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Message,PhoneNumber")] Vivscontactu vivscontactu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivscontactu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vivscontactu);
        }

        // GET: Vivscontactus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivscontactu = await _context.Vivscontactus.FindAsync(id);
            if (vivscontactu == null)
            {
                return NotFound();
            }
            return View(vivscontactu);
        }

        // POST: Vivscontactus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Email,Message,PhoneNumber")] Vivscontactu vivscontactu)
        {
            if (id != vivscontactu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivscontactu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivscontactuExists(vivscontactu.Id))
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
            return View(vivscontactu);
        }

        // GET: Vivscontactus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivscontactu = await _context.Vivscontactus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivscontactu == null)
            {
                return NotFound();
            }

            return View(vivscontactu);
        }

        // POST: Vivscontactus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivscontactu = await _context.Vivscontactus.FindAsync(id);
            _context.Vivscontactus.Remove(vivscontactu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivscontactuExists(decimal id)
        {
            return _context.Vivscontactus.Any(e => e.Id == id);
        }
    }
}

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
    public class VivsaboutusController : Controller
    {
        private readonly ModelContext _context;

        public VivsaboutusController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivsaboutus
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vivsaboutus.ToListAsync());
        }

        // GET: Vivsaboutus/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsaboutu = await _context.Vivsaboutus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivsaboutu == null)
            {
                return NotFound();
            }

            return View(vivsaboutu);
        }

        // GET: Vivsaboutus/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vivsaboutus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Message,PhoneNumber,Address")] Vivsaboutu vivsaboutu)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivsaboutu);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vivsaboutu);
        }

        // GET: Vivsaboutus/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsaboutu = await _context.Vivsaboutus.FindAsync(id);
            if (vivsaboutu == null)
            {
                return NotFound();
            }
            return View(vivsaboutu);
        }

        // POST: Vivsaboutus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Name,Email,Message,PhoneNumber,Address")] Vivsaboutu vivsaboutu)
        {
            if (id != vivsaboutu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivsaboutu);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivsaboutuExists(vivsaboutu.Id))
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
            return View(vivsaboutu);
        }

        // GET: Vivsaboutus/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsaboutu = await _context.Vivsaboutus
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivsaboutu == null)
            {
                return NotFound();
            }

            return View(vivsaboutu);
        }

        // POST: Vivsaboutus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivsaboutu = await _context.Vivsaboutus.FindAsync(id);
            _context.Vivsaboutus.Remove(vivsaboutu);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivsaboutuExists(decimal id)
        {
            return _context.Vivsaboutus.Any(e => e.Id == id);
        }
    }
}

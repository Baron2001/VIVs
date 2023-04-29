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
    public class VivshomesController : Controller
    {
        private readonly ModelContext _context;

        public VivshomesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivshomes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vivshomes.ToListAsync());
        }

        // GET: Vivshomes/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivshome = await _context.Vivshomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivshome == null)
            {
                return NotFound();
            }

            return View(vivshome);
        }

        // GET: Vivshomes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vivshomes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Image1,Title1,Image2,Title2,Image3,Title3")] Vivshome vivshome)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivshome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vivshome);
        }

        // GET: Vivshomes/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivshome = await _context.Vivshomes.FindAsync(id);
            if (vivshome == null)
            {
                return NotFound();
            }
            return View(vivshome);
        }

        // POST: Vivshomes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Image1,Title1,Image2,Title2,Image3,Title3")] Vivshome vivshome)
        {
            if (id != vivshome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivshome);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivshomeExists(vivshome.Id))
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
            return View(vivshome);
        }

        // GET: Vivshomes/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivshome = await _context.Vivshomes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vivshome == null)
            {
                return NotFound();
            }

            return View(vivshome);
        }

        // POST: Vivshomes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivshome = await _context.Vivshomes.FindAsync(id);
            _context.Vivshomes.Remove(vivshome);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivshomeExists(decimal id)
        {
            return _context.Vivshomes.Any(e => e.Id == id);
        }
    }
}

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
    public class VivscategoriesController : Controller
    {
        private readonly ModelContext _context;

        public VivscategoriesController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivscategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vivscategories.ToListAsync());
        }

        // GET: Vivscategories/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivscategory = await _context.Vivscategories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (vivscategory == null)
            {
                return NotFound();
            }

            return View(vivscategory);
        }

        // GET: Vivscategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vivscategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Categoryid,Categoryname,Categoryimagepath")] Vivscategory vivscategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivscategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vivscategory);
        }

        // GET: Vivscategories/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivscategory = await _context.Vivscategories.FindAsync(id);
            if (vivscategory == null)
            {
                return NotFound();
            }
            return View(vivscategory);
        }

        // POST: Vivscategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Categoryid,Categoryname,Categoryimagepath")] Vivscategory vivscategory)
        {
            if (id != vivscategory.Categoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivscategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivscategoryExists(vivscategory.Categoryid))
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
            return View(vivscategory);
        }

        // GET: Vivscategories/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivscategory = await _context.Vivscategories
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (vivscategory == null)
            {
                return NotFound();
            }

            return View(vivscategory);
        }

        // POST: Vivscategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivscategory = await _context.Vivscategories.FindAsync(id);
            _context.Vivscategories.Remove(vivscategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivscategoryExists(decimal id)
        {
            return _context.Vivscategories.Any(e => e.Categoryid == id);
        }
    }
}

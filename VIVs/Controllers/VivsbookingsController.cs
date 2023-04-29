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
    public class VivsbookingsController : Controller
    {
        private readonly ModelContext _context;

        public VivsbookingsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivsbookings
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Vivsbookings.Include(v => v.Post).Include(v => v.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: Vivsbookings/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsbooking = await _context.Vivsbookings
                .Include(v => v.Post)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Bookid == id);
            if (vivsbooking == null)
            {
                return NotFound();
            }

            return View(vivsbooking);
        }

        // GET: Vivsbookings/Create
        public IActionResult Create()
        {
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid");
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid");
            return View();
        }

        // POST: Vivsbookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Bookid,Booktime,Postid,Userid")] Vivsbooking vivsbooking)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivsbooking);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid", vivsbooking.Postid);
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivsbooking.Userid);
            return View(vivsbooking);
        }

        // GET: Vivsbookings/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsbooking = await _context.Vivsbookings.FindAsync(id);
            if (vivsbooking == null)
            {
                return NotFound();
            }
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid", vivsbooking.Postid);
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivsbooking.Userid);
            return View(vivsbooking);
        }

        // POST: Vivsbookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Bookid,Booktime,Postid,Userid")] Vivsbooking vivsbooking)
        {
            if (id != vivsbooking.Bookid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivsbooking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivsbookingExists(vivsbooking.Bookid))
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
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid", vivsbooking.Postid);
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivsbooking.Userid);
            return View(vivsbooking);
        }

        // GET: Vivsbookings/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsbooking = await _context.Vivsbookings
                .Include(v => v.Post)
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Bookid == id);
            if (vivsbooking == null)
            {
                return NotFound();
            }

            return View(vivsbooking);
        }

        // POST: Vivsbookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivsbooking = await _context.Vivsbookings.FindAsync(id);
            _context.Vivsbookings.Remove(vivsbooking);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivsbookingExists(decimal id)
        {
            return _context.Vivsbookings.Any(e => e.Bookid == id);
        }
    }
}

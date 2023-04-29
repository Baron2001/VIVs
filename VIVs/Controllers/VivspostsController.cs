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
    public class VivspostsController : Controller
    {
        private readonly ModelContext _context;

        public VivspostsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivsposts
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Vivsposts.Include(v => v.Users);
            return View(await modelContext.ToListAsync());
        }

        // GET: Vivsposts/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivspost = await _context.Vivsposts
                .Include(v => v.Users)
                .FirstOrDefaultAsync(m => m.Postid == id);
            if (vivspost == null)
            {
                return NotFound();
            }

            return View(vivspost);
        }

        // GET: Vivsposts/Create
        public IActionResult Create()
        {
            ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid");
            return View();
        }

        // POST: Vivsposts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Postid,Numberofitem,Description,Deadline,Postdate,Isdeleted,Usersid")] Vivspost vivspost)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivspost);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivspost.Usersid);
            return View(vivspost);
        }

        // GET: Vivsposts/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivspost = await _context.Vivsposts.FindAsync(id);
            if (vivspost == null)
            {
                return NotFound();
            }
            ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivspost.Usersid);
            return View(vivspost);
        }

        // POST: Vivsposts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Postid,Numberofitem,Description,Deadline,Postdate,Isdeleted,Usersid")] Vivspost vivspost)
        {
            if (id != vivspost.Postid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivspost);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivspostExists(vivspost.Postid))
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
            ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivspost.Usersid);
            return View(vivspost);
        }

        // GET: Vivsposts/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivspost = await _context.Vivsposts
                .Include(v => v.Users)
                .FirstOrDefaultAsync(m => m.Postid == id);
            if (vivspost == null)
            {
                return NotFound();
            }

            return View(vivspost);
        }

        // POST: Vivsposts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivspost = await _context.Vivsposts.FindAsync(id);
            _context.Vivsposts.Remove(vivspost);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivspostExists(decimal id)
        {
            return _context.Vivsposts.Any(e => e.Postid == id);
        }
    }
}

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
    public class VivsusersController : Controller
    {
        private readonly ModelContext _context;

        public VivsusersController(ModelContext context)
        {
            _context = context;
        }

        // GET: Vivsusers
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City);
            return View(await modelContext.ToListAsync());
        }

        // GET: Vivsusers/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsuser = await _context.Vivsusers
                .Include(v => v.Categorytype)
                .Include(v => v.City)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (vivsuser == null)
            {
                return NotFound();
            }

            return View(vivsuser);
        }

        // GET: Vivsusers/Create
        public IActionResult Create()
        {
            ViewData["Categorytypeid"] = new SelectList(_context.Vivscategories, "Categoryid", "Categoryid");
            ViewData["Cityid"] = new SelectList(_context.Vivscities, "Cityid", "Cityid");
            return View();
        }

        // POST: Vivsusers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Userid,Fullname,Email,Username,Password,Confirmpassword,Phonenumber,Image,Gender,Isdeleted,Status,Verifycode,Establishmentnationalnumber,Otherscategory,Address,Categorytypeid,Cityid,Estabname")] Vivsuser vivsuser)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vivsuser);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Categorytypeid"] = new SelectList(_context.Vivscategories, "Categoryid", "Categoryid", vivsuser.Categorytypeid);
            ViewData["Cityid"] = new SelectList(_context.Vivscities, "Cityid", "Cityid", vivsuser.Cityid);
            
            return View(vivsuser);
        }

        // GET: Vivsusers/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsuser = await _context.Vivsusers.FindAsync(id);
            if (vivsuser == null)
            {
                return NotFound();
            }
            ViewData["Categorytypeid"] = new SelectList(_context.Vivscategories, "Categoryid", "Categoryid", vivsuser.Categorytypeid);
            ViewData["Cityid"] = new SelectList(_context.Vivscities, "Cityid", "Cityid", vivsuser.Cityid);
            return View(vivsuser);
        }

        // POST: Vivsusers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Userid,Fullname,Email,Username,Password,Confirmpassword,Phonenumber,Image,Gender,Isdeleted,Status,Verifycode,Establishmentnationalnumber,Otherscategory,Address,Categorytypeid,Cityid,Estabname")] Vivsuser vivsuser)
        {
            if (id != vivsuser.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vivsuser);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VivsuserExists(vivsuser.Userid))
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
            ViewData["Categorytypeid"] = new SelectList(_context.Vivscategories, "Categoryid", "Categoryid", vivsuser.Categorytypeid);
            ViewData["Cityid"] = new SelectList(_context.Vivscities, "Cityid", "Cityid", vivsuser.Cityid);
            return View(vivsuser);
        }

        // GET: Vivsusers/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsuser = await _context.Vivsusers
                .Include(v => v.Categorytype)
                .Include(v => v.City)
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (vivsuser == null)
            {
                return NotFound();
            }

            return View(vivsuser);
        }

        // POST: Vivsusers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var vivsuser = await _context.Vivsusers.FindAsync(id);
            _context.Vivsusers.Remove(vivsuser);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VivsuserExists(decimal id)
        {
            return _context.Vivsusers.Any(e => e.Userid == id);
        }
    }
}

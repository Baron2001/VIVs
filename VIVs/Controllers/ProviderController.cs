using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using VIVs.Models;

namespace VIVs.Controllers
{
    public class ProviderController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProviderController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        ///////////////////////////////////////////////////////////////
        public IActionResult Index()
        {
            ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
            ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
            ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");
            var modelContext = _context.Vivsposts.Where(v => v.Usersid == HttpContext.Session.GetInt32("ProviderId")&& v.Isdeleted==false).ToList();
            //var post = modelContext.Where(i => i.Usersid == HttpContext.Session.GetInt32("ProviderId")).ToList();
            return View(modelContext);
        }
        ///////////////////////////////////////////////////////////////
        // GET: Vivsposts/Create
        public IActionResult Create()
        {
            ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
            ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
            ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");
            return View();
        }
        // POST: Vivsposts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Vivspost vivspost)
        {
            if (ModelState.IsValid)
            {
                ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
                ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
                ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");
                vivspost.Usersid = HttpContext.Session.GetInt32("ProviderId");
                vivspost.Postdate = DateTime.Now;
                vivspost.Isdeleted = false;
                _context.Add(vivspost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Provider");
            }
            //ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivspost.Usersid);
            return View(vivspost);
        }
        ///////////////////////////////////////////////////////////////
        // GET: Vivsposts/Edit/5
        public async Task<IActionResult> PostEdit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
            ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
            ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");
            var vivspost = await _context.Vivsposts.FindAsync(id);
            if (vivspost == null)
            {
                return NotFound();
            }
            ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivspost.Usersid);
            return View(vivspost);
        }
        // POST: Vivsposts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PostEdit(decimal id, Vivspost vivspost)
        {
            if (id != vivspost.Postid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
                ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
                ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");
                vivspost.Postdate= DateTime.Now;
                vivspost.Usersid = HttpContext.Session.GetInt32("ProviderId");
                _context.Update(vivspost);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Provider");
            }
            ViewData["Usersid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivspost.Usersid);
            return View(vivspost);
        }
        ///////////////////////////////////////////////////////////////
        public IActionResult Isdeleted(decimal? id)
        {
            ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
            ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
            ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");
            var post = _context.Vivsposts.Where(v => v.Postid == id).FirstOrDefault();
            post.Isdeleted = true;
            _context.Update(post);
            _context.SaveChangesAsync();
            return RedirectToAction("Index", "Provider");
        }
    }
}

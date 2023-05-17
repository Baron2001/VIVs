using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VIVs.Models;

namespace VIVs.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnviroment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            //
            ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
            ViewBag.ReceiverName = HttpContext.Session.GetString("ReceiverName");
            if (HttpContext.Session.GetString("ReceiverImage") != null)
            {
                ViewBag.ReceiverImage = HttpContext.Session.GetString("ReceiverImage");
            }
            ViewBag.ReceiverEmail = HttpContext.Session.GetString("ReceiverEmail");

            ViewBag.NumberOfProvider = _context.Vivslogins.Where(r => r.Rolesid == 2).Count();
            ViewBag.NumberOfResevar = _context.Vivslogins.Where(r => r.Rolesid == 3).Count();
            ViewBag.AllPost = _context.Vivsposts.Select(b => b.Postid).Count();
            var Home = _context.Vivshomes.FirstOrDefault();
            var cat = _context.Vivscategories.FirstOrDefault();
            var AboutUs = _context.Vivsaboutus.FirstOrDefault();
            var ContactUs = _context.Vivscontactus.ToList();
            var Post = _context.Vivsposts.Include(v => v.Users).ToList().Take(6).ToList(); 
            var model = Tuple.Create<Vivshome, Vivscategory, Vivsaboutu, IEnumerable<Vivscontactu>, IEnumerable<Vivspost>>(Home, cat, AboutUs, ContactUs, Post);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Vivscontactu vivscontactu)
        {
            ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
            ViewBag.ReceiverName = HttpContext.Session.GetString("ReceiverName");
            if (HttpContext.Session.GetString("ReceiverImage") != null)
            {
                ViewBag.ReceiverImage = HttpContext.Session.GetString("ReceiverImage");
            }
            ViewBag.ReceiverEmail = HttpContext.Session.GetString("ReceiverEmail");
            if (ModelState.IsValid)
            {
                _context.Add(vivscontactu);
                await _context.SaveChangesAsync();
            }
            ViewBag.NumberOfProvider = _context.Vivslogins.Where(r => r.Rolesid == 2).Count();
            ViewBag.NumberOfResevar = _context.Vivslogins.Where(r => r.Rolesid == 3).Count();
            ViewBag.AllPost = _context.Vivsposts.Select(b => b.Postid).Count();
            var Home = _context.Vivshomes.FirstOrDefault();
            var cat = _context.Vivscategories.FirstOrDefault();
            var AboutUs = _context.Vivsaboutus.FirstOrDefault();
            var ContactUs = _context.Vivscontactus.ToList();
            var Post = _context.Vivsposts.Include(v => v.Users).ToList().Take(6).ToList();
            var model = Tuple.Create<Vivshome, Vivscategory, Vivsaboutu, IEnumerable<Vivscontactu> , IEnumerable<Vivspost>>(Home, cat, AboutUs, ContactUs, Post);
            return View(model);
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

        public IActionResult Post()
        {
            ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
            ViewBag.ReceiverName = HttpContext.Session.GetString("ReceiverName");
            if (HttpContext.Session.GetString("ReceiverImage") != null)
            {
                ViewBag.ReceiverImage = HttpContext.Session.GetString("ReceiverImage");
            }
            ViewBag.ReceiverEmail = HttpContext.Session.GetString("ReceiverEmail");
            
            var Medical = _context.Vivsposts.Include(v => v.Users).Where(c => c.Users.Categorytypeid ==1).ToList();
            var Pharmacies = _context.Vivsposts.Include(v => v.Users).Where(c => c.Users.Categorytypeid == 3).ToList();
            var Restaurants = _context.Vivsposts.Include(v => v.Users).Where(c => c.Users.Categorytypeid == 2).ToList();
            var Other = _context.Vivsposts.Include(v => v.Users).Where(c => c.Users.Categorytypeid == 4).ToList();
            var model3 = Tuple.Create< IEnumerable<Vivspost>, IEnumerable<Vivspost>, IEnumerable<Vivspost>, IEnumerable<Vivspost>>(Medical, Pharmacies, Restaurants, Other);
            return View(model3);
        }

        public async Task<IActionResult> BookCreate(decimal id)
        {
            //Bookid
            //Booktime  
            //Postid   
            //Userid 
            //Post
            //User
            Vivsbooking vivsbooking =new Vivsbooking();
            var PostList = _context.Vivsposts.Include(v => v.Users).Where(p => p.Postid == id && p.Numberofitem > 0 && p.Deadline >=DateTime.Now).FirstOrDefault();

            if (ModelState.IsValid)
            {
                try
                {
                    
                    ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
                    if (PostList != null)
                    {
                        vivsbooking.Booktime= DateTime.Now;
                        vivsbooking.Postid= id;
                        vivsbooking.Userid = HttpContext.Session.GetInt32("ReceiverId");
                        PostList.Numberofitem -= 1;
                        PostList.Isdeleted = false;
                        _context.Update(PostList);
                        await _context.SaveChangesAsync();
                        _context.Add(vivsbooking);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid", vivsbooking.Postid);
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivsbooking.Userid);
            return RedirectToAction("Post", "Home");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

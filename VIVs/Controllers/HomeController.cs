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
            ViewBag.messageBookingHomebefore = HttpContext.Session.GetString("messageBookingHomebefore");
            HttpContext.Session.Remove("messageBookingHomebefore");

            ViewBag.messageBookingHomeDone = HttpContext.Session.GetString("messageBookingHomeDone");
            HttpContext.Session.Remove("messageBookingHomeDone");

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
            var post = _context.Vivsposts.Include(v => v.Users).Where(a => a.Numberofitem > 0 && a.Deadline >= DateTime.Now).ToList();
            var Post = post.Take(6).ToList();
            var model = Tuple.Create<Vivshome, Vivscategory, Vivsaboutu, IEnumerable<Vivscontactu>, IEnumerable<Vivspost>>(Home, cat, AboutUs, ContactUs, Post);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Vivscontactu vivscontactu)
        {
            ViewBag.messageBookingHomebefore = HttpContext.Session.GetString("messageBookingHomebefore");
            HttpContext.Session.Remove("messageBookingHomebefore");

            ViewBag.messageBookingHomeDone = HttpContext.Session.GetString("messageBookingHomeDone");
            HttpContext.Session.Remove("messageBookingHomeDone");

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
            var post = _context.Vivsposts.Include(v => v.Users).Where(a => a.Numberofitem > 0 && a.Deadline >= DateTime.Now).ToList();
            var Post = post.Take(6).ToList();
            var model = Tuple.Create<Vivshome, Vivscategory, Vivsaboutu, IEnumerable<Vivscontactu>, IEnumerable<Vivspost>>(Home, cat, AboutUs, ContactUs, Post);
            return View(model);
        }

        public async Task<IActionResult> BookCreateHome(decimal id)
        {
            Vivsbooking vivsbooking = new Vivsbooking();
            var PostList = _context.Vivsposts.Include(v => v.Users).Where(p => p.Postid == id && p.Numberofitem > 0 && p.Deadline >= DateTime.Now).FirstOrDefault();

            if (ModelState.IsValid)
            {
                try
                {
                    var Booking = _context.Vivsbookings.Where(a => a.Userid == HttpContext.Session.GetInt32("ReceiverId") && a.Postid == id).FirstOrDefault();
                    ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
                    if (Booking != null)
                    {
                        HttpContext.Session.Remove("messageBookingHomeDone");

                        HttpContext.Session.SetString("messageBookingHomebefore", "you have booked this item before");
                        ViewBag.messageBookingHomebefore = HttpContext.Session.GetString("messageBookingHomebefore");
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        if (PostList != null)
                        {

                            vivsbooking.Booktime = DateTime.Now;
                            vivsbooking.Postid = id;
                            vivsbooking.Userid = HttpContext.Session.GetInt32("ReceiverId");
                            PostList.Numberofitem -= 1;
                            PostList.Isdeleted = false;
                            _context.Update(PostList);
                            await _context.SaveChangesAsync();
                            _context.Add(vivsbooking);
                            await _context.SaveChangesAsync();
                            HttpContext.Session.Remove("messageBookingHomebefore");

                            HttpContext.Session.SetString("messageBookingHomeDone", "you Book Is Done");
                            ViewBag.messageBookingHomeDone = HttpContext.Session.GetString("messageBookingHomeDone");
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid", vivsbooking.Postid);
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivsbooking.Userid);
            return View();
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


            ViewBag.messageBooking = HttpContext.Session.GetString("messageBooking");
            HttpContext.Session.Remove("messageBooking");

            ViewBag.messageBookingpostDone = HttpContext.Session.GetString("messageBookingpostDone");
            HttpContext.Session.Remove("messageBookingpostDone");


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
            Vivsbooking vivsbooking = new Vivsbooking();
            var PostList = _context.Vivsposts.Include(v => v.Users).Where(p => p.Postid == id && p.Numberofitem > 0 && p.Deadline >= DateTime.Now).FirstOrDefault();

            if (ModelState.IsValid)
            {
                try
                {
                    var Booking = _context.Vivsbookings.Where(a => a.Userid == HttpContext.Session.GetInt32("ReceiverId") && a.Postid == id).FirstOrDefault();
                    ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
                    if (Booking != null)
                    {
                        HttpContext.Session.SetString("messageBooking", "you have booked this item before");
                        ViewBag.messageBooking = HttpContext.Session.GetString("messageBooking");
                        HttpContext.Session.Remove("messageBookingpostDone");
                        return RedirectToAction("Post", "Home");
                    }
                    else
                    {
                        if (PostList != null)
                        {

                            vivsbooking.Booktime = DateTime.Now;
                            vivsbooking.Postid = id;
                            vivsbooking.Userid = HttpContext.Session.GetInt32("ReceiverId");
                            PostList.Numberofitem -= 1;
                            PostList.Isdeleted = false;
                            _context.Update(PostList);
                            await _context.SaveChangesAsync();
                            _context.Add(vivsbooking);
                            await _context.SaveChangesAsync();
                            HttpContext.Session.Remove("messageBooking");


                            HttpContext.Session.SetString("messageBookingpostDone", "you Book Is Done");
                            ViewBag.messageBookingpostDone = HttpContext.Session.GetString("messageBookingpostDone");
          
                            return RedirectToAction("Post", "Home");
                        }
                        else
                        {

                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            ViewData["Postid"] = new SelectList(_context.Vivsposts, "Postid", "Postid", vivsbooking.Postid);
            ViewData["Userid"] = new SelectList(_context.Vivsusers, "Userid", "Userid", vivsbooking.Userid);
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

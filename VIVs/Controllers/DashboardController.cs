using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using VIVs.Models;

namespace VIVs.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ModelContext _context;

        public DashboardController(ModelContext context)
        {
            _context = context;
        }
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        public IActionResult ProviderArchive()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Accept" || s.Status == "Reject" ).ToList();
            //var Rolesses = _context.Rolesses.ToList();

            //ViewBag.ReceiverId = HttpContext.Session.GetInt32("ReceiverId");
            //ViewBag.ReceiverName = HttpContext.Session.GetString("ReceiverName");
            //ViewBag.ReceiverImage = HttpContext.Session.GetString("ReceiverImage");

            //ViewBag.ProviderId = HttpContext.Session.GetInt32("ProviderId");
            //ViewBag.ProviderName = HttpContext.Session.GetString("ProviderName");
            //ViewBag.ProviderImage = HttpContext.Session.GetString("ProviderImage");

            ViewBag.AdminId = HttpContext.Session.GetInt32("AdminId");
            ViewBag.AdminName = HttpContext.Session.GetString("AdminName");
            ViewBag.AdminImage = HttpContext.Session.GetString("AdminImage");


            //var modelContext = _context.Logins.Include(l => l.Roles).Include(l => l.User);
            return View(User);
        }
        public IActionResult ProviderRequests()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Waiting").ToList();

            return View(User);
        }
        public IActionResult ReceiverArchive()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Accept" || s.Status == "Reject").ToList();

            return View(User);
        }
        public IActionResult ReceiverRequests()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Waiting").ToList();

            return View(User);
        }
    }
}

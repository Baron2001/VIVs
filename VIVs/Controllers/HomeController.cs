using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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
            var AboutUs = _context.Vivsaboutus.FirstOrDefault();
            var Contact = _context.Vivscontactus.ToList();
            var Categories = _context.Vivscategories.ToList();
            var Home = _context.Vivshomes.FirstOrDefault();
            var Post = _context.Vivsposts.ToList();
            var City = _context.Vivscities.ToList();
            var Users = _context.Vivsusers.ToList();
            var model3 = Tuple.Create<Vivsaboutu, IEnumerable<Vivscontactu>, IEnumerable<Vivscategory>, Vivshome, IEnumerable<Vivspost>, IEnumerable<Vivscity>, IEnumerable<Vivsuser>>(AboutUs, Contact, Categories, Home, Post, City, Users);
            return View(model3);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

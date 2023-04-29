using Microsoft.AspNetCore.Mvc;

namespace VIVs.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}

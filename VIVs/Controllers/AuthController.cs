using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using VIVs.Models;

namespace VIVs.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public AuthController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Register()
        {
            ViewBag.error = HttpContext.Session.GetString("message");
            HttpContext.Session.Remove("message");
            return View();
        }
        [HttpPost]
        public IActionResult Register(Vivsuser User, string Email, string password, string Estabname ,string Status)
        {
            var user = _context.Vivslogins.Where(x => x.Email == Email).SingleOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetString("message", "Email already used");
                ViewBag.error = HttpContext.Session.GetString("message");
                ModelState.AddModelError(Email, "Usename already used");
                return View();
            }
            if (ModelState.IsValid)
            {
                if (User.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath; // wwwroot 
                    string fileName = Guid.NewGuid().ToString() + "_" + User.ImageFile.FileName; // sffjhfbvjhbjskdnklnklnlk_picture 
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName); // wwwroot/image/filename

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        User.ImageFile.CopyTo(fileStream);
                    }
                    User.Image = fileName;
                }
                HttpContext.Session.Remove("message");
                User.Status = "Waiting";
                _context.Add(User);
                _context.SaveChangesAsync();
                Vivslogin Login = new Vivslogin();
                Login.Username = User.Username;
                Login.Email = Email;
                Login.Password = password;
                Login.Usersid = User.Userid;
                if ( Estabname != null) 
                {
                    Login.Rolesid = 2; //As Provider
                }
                else 
                { 
                    Login.Rolesid = 3; //As Receiver 
                }
                
                _context.Add(Login);
                _context.SaveChanges();
                //return RedirectToAction(nameof(Register));
                return RedirectToAction("Login", "Auth");
            }
            return View(User);
        }


        [HttpPost]
        public IActionResult Login(Vivslogin login)
        {
            if (ModelState.IsValid)
            {
                // حددو اذا بدكم اتسجلو الدخول بال Email or Username    ||||||  x.Username == login.Username &&
                var auth = _context.Vivslogins.Include(z => z.Users).Where(x =>  x.Email == login.Email && x.Password == login.Password).SingleOrDefault();
                if (auth != null)
                {
                    switch (auth.Rolesid)
                    {
                        case 1: //As Admin 
                            HttpContext.Session.SetInt32("AdminId", (int)auth.Usersid);
                            HttpContext.Session.SetString("AdminName", auth.Users.Fullname);
                            if (auth.Users.Image != null)
                            {
                                HttpContext.Session.SetString("AdminImage", auth.Users.Image);
                            }
                            return RedirectToAction("AdminDashBoard", "DashBoard");

                        case 2: //As Provider
                            HttpContext.Session.SetInt32("ProviderId", (int)auth.Usersid);
                            HttpContext.Session.SetString("ProviderName", auth.Users.Fullname);
                            if (auth.Users.Image != null)
                            {
                                HttpContext.Session.SetString("ProviderImage", auth.Users.Image);
                            }
                            HttpContext.Session.SetString("ProviderEmail", auth.Email);
                            return RedirectToAction("ProviderDashBoard", "DashBoard");

                        case 3: //As Receiver 
                            HttpContext.Session.SetInt32("ReceiverId", (int)auth.Usersid);
                            HttpContext.Session.SetString("ReceiverName", auth.Users.Fullname);
                            if (auth.Users.Image != null)
                            {
                                HttpContext.Session.SetString("ReceiverImage", auth.Users.Image);
                            }
                            HttpContext.Session.SetString("ReceiverEmail", auth.Email);
                            return RedirectToAction("Home", "Home");
                    }
                    HttpContext.Session.Remove("messageLogIn");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    HttpContext.Session.SetString("messageLogIn", "Email or Password is wrong");
                    ViewBag.error = HttpContext.Session.GetString("messageLogIn");
                }
            }

            return View();
        }

        public IActionResult Login()
        {
            ViewBag.error = HttpContext.Session.GetString("messageLogIn");
            HttpContext.Session.Remove("messageLogIn");
            return View();
        }
        public IActionResult Logout()
        {

            //AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }

        private bool UserExists(decimal id)
        {
            return _context.Vivsusers.Any(e => e.Userid == id);
        }
    }
}

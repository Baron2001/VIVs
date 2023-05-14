using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VIVs.Models;
using EmailServices;
using MimeKit;
using MailKit.Net.Smtp;

namespace VIVs.Controllers
{
    public class AuthController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IEmailSender _emailSender;
        public AuthController(ModelContext context, IWebHostEnvironment webHostEnvironment , IEmailSender emailSender)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _emailSender = emailSender;
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        
        public IActionResult Register()
        {
            ViewData["Categorytypeid"] = new SelectList(_context.Vivscategories, "Categoryid", "Categoryname");
            ViewData["Cityid"] = new SelectList(_context.Vivscities, "Cityid", "City");
            ViewBag.error = HttpContext.Session.GetString("message");
            HttpContext.Session.Remove("message");
            return View();
        }
        [HttpPost]
        public IActionResult Register(Vivsuser User, string Email, string password, string Estabname ,string Status, string FirstName, string MiddleName, string Surname, string LastName)
        {
            ViewData["Categorytypeid"] = new SelectList(_context.Vivscategories, "Categoryid", "Categoryname");
            ViewData["Cityid"] = new SelectList(_context.Vivscities, "Cityid", "City");
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
                string full = FirstName + " " + MiddleName + " " + Surname + " " +LastName;
                User.Fullname = full;
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
        
        ///////////////////////////////////////////////////////////////////////////////////////
        
        public IActionResult Login()
        {
            ViewBag.error = HttpContext.Session.GetString("messageLogIn");
            HttpContext.Session.Remove("messageLogIn");
            return View();
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
                    if (auth.Users.Status == "Accept")
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
                                return RedirectToAction("Admin", "DashBoard");
                            case 2: //As Provider
                                HttpContext.Session.SetInt32("ProviderId", (int)auth.Usersid);
                                HttpContext.Session.SetString("ProviderName", auth.Users.Fullname);
                                if (auth.Users.Image != null)
                                {
                                    HttpContext.Session.SetString("ProviderImage", auth.Users.Image);
                                }
                                HttpContext.Session.SetString("ProviderEmail", auth.Email);
                                return RedirectToAction("Index", "Provider");
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
                    }
                    else if (auth.Users.Status == "Reject")
                    {
                        return RedirectToAction("PageReject", "Auth");
                    }
                    else
                    {
                        return RedirectToAction("PageWaiting", "Auth");
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

        ///////////////////////////////////////////////////////////////////////////////////////
        
        public IActionResult Logout()
        {
            //AuthenticationHttpContextExtensions.SignOutAsync(HttpContext, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
        
        /////////////////////////////////////////////////////////////////////////////////////// 
        
        public IActionResult PageWaiting()
        {
            return View();
        }
        public IActionResult PageReject()
        {
            return View();
        }
        
        /////////////////////////////////////////////////////////////////////////////////////// 
        
        [HttpGet]
        public IActionResult ResetPassword()
        {
            ViewBag.errorReset = HttpContext.Session.GetString("messageReset");
            HttpContext.Session.Remove("messageReset");
            //ViewBag.SendEmail = HttpContext.Session.GetString("SendEmail");
            //HttpContext.Session.Remove("SendEmail");
            //HttpContext.Session.Clear();
            HttpContext.Session.GetInt32("ResetPasswordUserId");
            return View();
        }
        [HttpPost]
        public IActionResult ResetPassword(string Email)
        {
            var user = _context.Vivsusers.Where(v => v.Email == Email).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetInt32("ResetPasswordUserId", (int)user.Userid);
                // define the 4-digit range
                int minNumber = 1000;
                int maxNumber = 9999;

                // generate a random number from the range
                Random random = new Random();
                int randomNumber = random.Next(minNumber, maxNumber + 1);

                SendEmail(user.Email, randomNumber);
                //HttpContext.Session.SetString("SendEmail", "The Verify Code is sent to This Email "+ user.Email);
                //ViewBag.SendEmail = HttpContext.Session.GetString("SendEmail");
                user.Verifycode = randomNumber.ToString();
                _context.Update(user);
                _context.SaveChangesAsync();
                return RedirectToAction("SendCode", "Auth");
            }
            else
            {
                HttpContext.Session.SetString("messageReset", "Your Email is not valid");
                ViewBag.errorReset = HttpContext.Session.GetString("messageReset");
            }
            return RedirectToAction("ResetPassword", "Auth");
        }

        ////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public void SendEmail(String ParentEmail, int Code)
        {
            //"anoodgg@yahoo.com"
            //"ozwlzqmtasgevhbq"
            MimeMessage message = new MimeMessage();
            MailboxAddress from = new MailboxAddress("VIV`s", "s.moe12@yahoo.com");
            message.From.Add(from);
            MailboxAddress to = new MailboxAddress("User", ParentEmail);
            message.To.Add(to);
            message.Subject = "Verify Code";
            BodyBuilder bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody =
            "<p>Your verify code is:<b style=\"color:#7fb685\">" + Code + "</b> </p>" ;
                //bodyBuilder.HtmlBody =
                //"<p>Your Garage Status is: <b style=\"color:red\">Reject</b></p> ";
            message.Body = bodyBuilder.ToMessageBody();
            using (var clinte = new SmtpClient())
            {
                clinte.Connect("smtp.mail.yahoo.com", 465, true);
                clinte.Authenticate("s.moe12@yahoo.com", "rxlhovtglvjibneg");
                clinte.Send(message);
                clinte.Disconnect(true);
            }
        }

        ////////////////////////////////////////////////////////////////////////
        [HttpGet]
        public IActionResult SendCode()
        {
            ViewBag.errorSendCode = HttpContext.Session.GetString("messageSendCode");
            HttpContext.Session.Remove("messageSendCode");

            HttpContext.Session.GetInt32("UserSetId");
            HttpContext.Session.GetInt32("ResetPasswordUserId");

            return View();
        }
        [HttpPost]
        public IActionResult SendCode(string Verifycode)
        {
            var user = _context.Vivsusers.Where(v => v.Userid == HttpContext.Session.GetInt32("ResetPasswordUserId") && v.Verifycode == Verifycode).FirstOrDefault();
            if (user != null)
            {
                HttpContext.Session.SetInt32("UserSetId", (int)user.Userid);
                return RedirectToAction("SetPass", "Auth");
            }
            else
            {
                HttpContext.Session.SetString("messageSendCode", "The number you entered doesn’t match your code. Please try again.");
                ViewBag.errorSendCode = HttpContext.Session.GetString("messageSendCode");
            }
            return RedirectToAction("SendCode", "Auth");
        }
        
        ///////////////////////////////////////////////////////////////////////
        
        [HttpGet]
        public IActionResult SetPass()
        {
            HttpContext.Session.GetInt32("UserSetId");
            ViewBag.UserSetId = HttpContext.Session.GetInt32("UserSetId");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SetPass(string Password, string Confirmpassword)
        {
            HttpContext.Session.GetInt32("UserSetId");
            ViewBag.UserSetId = HttpContext.Session.GetInt32("UserSetId");
            if (HttpContext.Session.GetInt32("UserSetId") != null)
            {
                var user = _context.Vivsusers.Where(v => v.Userid == HttpContext.Session.GetInt32("UserSetId")).FirstOrDefault();
                user.Password = Password;
                user.Confirmpassword = Confirmpassword;
                _context.Update(user);
                await _context.SaveChangesAsync();

                var Login = _context.Vivslogins.Where(v => v.Usersid == HttpContext.Session.GetInt32("UserSetId")).FirstOrDefault();
                //Login.Email = Email;
                Login.Password = Password;
                Login.Usersid = HttpContext.Session.GetInt32("UserSetId");
                _context.Update(Login);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Auth");
            }
            else 
            {
                return RedirectToAction("SendCode", "Auth");
            }
        }
        
        ///////////////////////////////////////////////////////////////////////
        private bool UserExists(decimal id)
        {
            return _context.Vivsusers.Any(e => e.Userid == id);
        }
    }
}

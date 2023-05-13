﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VIVs.Models;

namespace VIVs.Controllers
{
    public class DashboardController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public DashboardController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Admin()
        {
            return View();
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Profile(decimal? id)
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
            return View(vivsuser);
        }
        // POST: Doccustomers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile(decimal id, Vivsuser vivsuser, string Password, string Email)
        {
            if (id != vivsuser.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (vivsuser.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath; // wwwroot 
                    string fileName = Guid.NewGuid().ToString() + "_" + vivsuser.ImageFile.FileName; // sffjhfbvjhbjskdnklnklnlk_picture 
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName); // wwwroot/image/filename

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        vivsuser.ImageFile.CopyTo(fileStream);
                    }
                    vivsuser.Image = fileName;
                }
                else
                {
                    var cat = _context.Vivsusers.Where(c => c.Userid == id);
                    vivsuser.Image = cat.Select(u => u.Image).FirstOrDefault();
                }
                var login = _context.Vivslogins.Where(u => u.Usersid == id).FirstOrDefault();
                if (Password != null)
                {
                    login.Password = Password;
                }
                if (Email != null)
                {
                    login.Email = Email;

                }
                vivsuser.Fullname = "VIVs";
                vivsuser.Username = "VIVs";
                vivsuser.Categorytypeid = 4;
                vivsuser.Cityid = 1;
                vivsuser.Status = "Accept";
                vivsuser.Isdeleted = false;

                _context.Update(login);
                await _context.SaveChangesAsync();
                _context.Update(vivsuser);
                await _context.SaveChangesAsync();
                return RedirectToAction("Profile", "Dashboard");
            }
            return View(vivsuser);
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public IActionResult ProviderArchive()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => (s.Status == "Accept" || s.Status == "Reject") && s.Estabname != null).ToList();
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
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Waiting" && s.Estabname != null).ToList();

            return View(User);
        }
        public async Task<IActionResult> ProviderAcceptStatus(decimal id,  Vivsuser vivsuser)
        {
            var userList = _context.Vivsusers.Where(i => i.Userid == id).FirstOrDefault();

            if (id != userList.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vivsuser = userList;
                vivsuser.Status = "Accept";
                _context.Update(vivsuser);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProviderRequests", "Dashboard");
            }
            return View();
        }
        public async Task<IActionResult> ProviderRejectStatus(decimal id, Vivsuser vivsuser)
        {
            var userList = _context.Vivsusers.Where(i => i.Userid == id).FirstOrDefault();

            if (id != userList.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vivsuser = userList;
                vivsuser.Status = "Reject";
                _context.Update(vivsuser);
                await _context.SaveChangesAsync();
                return RedirectToAction("ProviderRequests", "Dashboard");
            }
            return View();
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public IActionResult ReceiverArchive()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Accept" || s.Status == "Reject" && s.Estabname == null).ToList();

            return View(User);
        }
        public IActionResult ReceiverRequests()
        {
            var User = _context.Vivsusers.Include(v => v.Categorytype).Include(v => v.City).Where(s => s.Status == "Waiting" && s.Estabname == null).ToList();

            return View(User);
        }
        public async Task<IActionResult> ReceiverAcceptStatus(decimal id, Vivsuser vivsuser)
        {
            var userList = _context.Vivsusers.Where(i => i.Userid == id).FirstOrDefault();

            if (id != userList.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vivsuser = userList;
                vivsuser.Status = "Accept";
                _context.Update(vivsuser);
                await _context.SaveChangesAsync();
                return RedirectToAction("ReceiverRequests", "Dashboard");
            }
            return View();
        }
        public async Task<IActionResult> ReceiverRejectStatus(decimal id, Vivsuser vivsuser)
        {
            var userList = _context.Vivsusers.Where(i => i.Userid == id).FirstOrDefault();

            if (id != userList.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                vivsuser = userList;
                vivsuser.Status = "Reject";
                _context.Update(vivsuser);
                await _context.SaveChangesAsync();
                return RedirectToAction("ReceiverRequests", "Dashboard");
            }
            return View();
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public async Task<IActionResult> Contact()
        {
            return View(await _context.Vivscontactus.ToListAsync());
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        // GET: Vivsaboutus/Edit/5
        public async Task<IActionResult> About(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivsaboutu = await _context.Vivsaboutus.FindAsync(id);
            if (vivsaboutu == null)
            {
                return NotFound();
            }
            return View(vivsaboutu);
        }
        // POST: Vivsaboutus/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> About(decimal id, Vivsaboutu vivsaboutu)
        {
            if (id != vivsaboutu.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(vivsaboutu);
                await _context.SaveChangesAsync();
            }
            return View(vivsaboutu);
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        public IActionResult Home()
        {
            return View(_context.Vivshomes.FirstOrDefault());
        }
        // GET: Vivshomes/Edit/5
        public async Task<IActionResult> EditeHome(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vivshome = await _context.Vivshomes.FindAsync(id);
            if (vivshome == null)
            {
                return NotFound();
            }

            return View(vivshome);
        }

        // POST: Vivshomes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditeHome(decimal id, Vivshome vivshome)
        {
            if (id != vivshome.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (vivshome.ImageFile1 != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + vivshome.ImageFile1.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await vivshome.ImageFile1.CopyToAsync(fileStream);
                    }
                    vivshome.Image1 = fileName;
                    vivshome.Image3 = _context.Vivshomes.Select(u => u.Image3).FirstOrDefault();
                    vivshome.Image2 = _context.Vivshomes.Select(u => u.Image2).FirstOrDefault();
                    //HttpContext.Session.SetString("Image1", home.Image1);
                    //HttpContext.Session.SetString("Image2", home.Image2);
                    //HttpContext.Session.SetString("Image3", home.Image3);
                    _context.Update(vivshome);
                    await _context.SaveChangesAsync();

                }
                if (vivshome.ImageFile2 != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + vivshome.ImageFile2.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await vivshome.ImageFile2.CopyToAsync(fileStream);
                    }
                    vivshome.Image2 = fileName;
                    vivshome.Image1 = _context.Vivshomes.Select(u => u.Image1).FirstOrDefault();
                    vivshome.Image3 = _context.Vivshomes.Select(u => u.Image3).FirstOrDefault();

                    _context.Update(vivshome);
                    await _context.SaveChangesAsync();

                }
                if (vivshome.ImageFile3 != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + vivshome.ImageFile3.FileName;
                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await vivshome.ImageFile3.CopyToAsync(fileStream);
                    }
                    vivshome.Image3 = fileName;
                    vivshome.Image2 = _context.Vivshomes.Select(u => u.Image2).FirstOrDefault();
                    vivshome.Image1 = _context.Vivshomes.Select(u => u.Image1).FirstOrDefault();
                    _context.Update(vivshome);
                    await _context.SaveChangesAsync();

                }

                if (vivshome.ImageFile1 == null && vivshome.ImageFile2 == null && vivshome.ImageFile3 == null)
                {
                    vivshome.Image1 = _context.Vivshomes.Select(u => u.Image1).FirstOrDefault();
                    vivshome.Image2 = _context.Vivshomes.Select(u => u.Image2).FirstOrDefault();
                    vivshome.Image3 = _context.Vivshomes.Select(u => u.Image3).FirstOrDefault();
                }
                _context.Update(vivshome);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Home));
            }
            return View(vivshome);
        }
        ///////////////////////////////////////////////////////////////////////////////////////
        





    }
}

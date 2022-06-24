using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStoreFP.Models;

namespace OnlineStoreFP.Controllers
{
    public class UserFpsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public UserFpsController(ModelContext context, IWebHostEnvironment _webHostEnviroment)
        {
            _context = context;
            this._webHostEnviroment = _webHostEnviroment;
        }

        // GET: UserFps
        public async Task<IActionResult> Index()
        {
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            var modelContext = _context.UserloginFps.Where(x=>x.Roleid!=1).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: UserFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFp = await _context.UserFps
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (userFp == null)
            {
                return NotFound();
            }

            return View(userFp);
        }

        // GET: UserFps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( UserFp userFp)
        {
            if (ModelState.IsValid)
            {
                if (userFp.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + userFp.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await userFp.ImageFile.CopyToAsync(fileStream);
                    }
                    userFp.Imagepath = fileName;
                    _context.Add(userFp);
                    await _context.SaveChangesAsync();
                }
                var check = _context.UserloginFps.Where(x => x.Email == userFp.Email).FirstOrDefault();



                if (check == null)
                {
                    var lastId = _context.UserFps.OrderByDescending(p => p.Userid).FirstOrDefault().Userid;

                    UserloginFp login = new UserloginFp();

                    login.Email = userFp.Email;
                    login.Password = userFp.Password;
                    login.Roleid = 1;
                    login.Userid = lastId;
                    _context.Add(login);
                    await _context.SaveChangesAsync();



                    //ViewBag.Successful = "The registration was successful, welcome " + customer.Fname;
                    //return RedirectToAction("Login", "LoginandRegistration");
                    return RedirectToAction(nameof(Index));
                }
                else
                {


                    ViewBag.InvaidUserName = "username should be unique";

                    return RedirectToAction(nameof(Index));
                }

                
            }
            return View(userFp);
        }

        // GET: UserFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFp = await _context.UserFps.FindAsync(id);
            if (userFp == null)
            {
                return NotFound();
            }
            return View(userFp);
        }

        // POST: UserFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  UserFp userFp)
        {
            if (id != userFp.Userid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                    {
               
                    if (userFp.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + userFp.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await userFp.ImageFile.CopyToAsync(fileStream);
                        }
                        userFp.Imagepath = fileName;
                       
                    }
                    else
                    {
                        userFp.Imagepath = (from c in _context.UserFps
                                                where c.Userid.Equals(id)
                                                select c.Imagepath).FirstOrDefault();
                    }
                    _context.Update(userFp);
                    await _context.SaveChangesAsync();

                    }

                catch (DbUpdateConcurrencyException)
                {
                    if (!UserFpExists(userFp.Userid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                var loginid = _context.UserloginFps.Where(x => x.Userid == id).FirstOrDefault(); 
                    
                    if(userFp.Email != null)
                    {
                        loginid.Email = userFp.Email;
                    }
                    if(userFp.Password != null)
                    {
                        loginid.Password = userFp.Password;
                    }
                    
                    
                    _context.Update(userFp);
                    await _context.SaveChangesAsync();
               
                return RedirectToAction(nameof(Index));
            }
            return View(userFp);
        }

        // GET: UserFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userFp = await _context.UserFps
                .FirstOrDefaultAsync(m => m.Userid == id);
            if (userFp == null)
            {
                return NotFound();
            }

            return View(userFp);
        }

        // POST: UserFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userFp = await _context.UserFps.FindAsync(id);
            _context.UserFps.Remove(userFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserFpExists(decimal id)
        {
            return _context.UserFps.Any(e => e.Userid == id);
        }
    }
}

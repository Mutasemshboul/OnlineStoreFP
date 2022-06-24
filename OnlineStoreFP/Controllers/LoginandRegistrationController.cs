using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OnlineStoreFP.Models;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreFP.Controllers
{
    public class LoginandRegistrationController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public LoginandRegistrationController(ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Register( UserFp user)
        {
            if (user.ImageFile != null)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string fileName = Guid.NewGuid().ToString() + "_" + user.ImageFile.FileName;
                string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await user.ImageFile.CopyToAsync(fileStream);
                }
                user.Imagepath = fileName;
                _context.Add(user);
                await _context.SaveChangesAsync();
            }



            var check = _context.UserloginFps.Where(x => x.Email == user.Email).FirstOrDefault();
            


            if (check == null)
            {
                var lastId = _context.UserFps.OrderByDescending(p => p.Userid).FirstOrDefault().Userid;

                UserloginFp login = new UserloginFp();

                login.Email = user.Email;
                login.Password = user.Password;
                login.Roleid = 2;
                login.Userid = lastId;
                _context.UserloginFps.Add(login);
                await _context.SaveChangesAsync();



                //ViewBag.Successful = "The registration was successful, welcome " + customer.Fname;
                //return RedirectToAction("Login", "LoginandRegistration");
                return RedirectToAction("Login", "LoginandRegistration");
            }
            else
            {


                ViewBag.InvaidUserName = "username should be unique";

                return View();
            }


        }
        [HttpPost]
        public IActionResult Login(UserloginFp uselLogin )
        {

            var auth = _context.UserloginFps.Where(x => x.Email == uselLogin.Email && x.Password == uselLogin.Password).SingleOrDefault();
            if (auth != null)
            {
                var name = _context.UserFps.Where(x => x.Userid == auth.Userid).SingleOrDefault();
                if (auth != null)
                {
                    if (auth.Roleid == 2)
                    {
                        HttpContext.Session.SetString("CustomerName", name.Fname + " " + name.Lname);
                        HttpContext.Session.SetString("CustomerEmail", auth.Email);
                        HttpContext.Session.SetString("CustomerPassword", auth.Password);
                        HttpContext.Session.SetInt32("CustomerId", (int)auth.Userid);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        HttpContext.Session.SetString("CustomerName", name.Fname + " " + name.Lname);
                        HttpContext.Session.SetString("CustomerEmail", auth.Email);
                        HttpContext.Session.SetString("CustomerPassword", auth.Password);
                        HttpContext.Session.SetInt32("CustomerId", (int)auth.Userid);
                        //return RedirectToAction("Index", "Uasedashbord");
                        return RedirectToAction("Index", "AdminDashbord");
                    }
                }
                else
                {
                    return RedirectToAction("Login", "LoginandRegistration");
                }
            }
            else
            {
                return RedirectToAction("Login", "LoginandRegistration");
            }
            
        }
    }
}

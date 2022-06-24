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
    public class UasedashbordController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;


        public UasedashbordController(ModelContext context, IWebHostEnvironment _webHostEnviroment)
        {
            _context = context;
            this._webHostEnviroment = _webHostEnviroment;
        }

        public IActionResult Index()
        {
            var custid = HttpContext.Session.GetInt32("CustomerId");
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            if (custid != null)
            {
                var item2 = _context.ProductuserFps.Where(x => x.Userid == custid).FirstOrDefault();
                if(item2 == null)
                {
                    return View();
                }

                var item3 = _context.ProductFps.Where(x => x.Productid == item2.Productid).FirstOrDefault();
                var item4 = _context.UserFps.Where(x=> x.Userid == custid).FirstOrDefault();
                if (item3 != null)
                {
                ViewBag.Userinfo = item2;
                ViewBag.Userinfo2 = item3;
                ViewBag.Userinfo3 = item4;


                    var customerproduct = _context.ProductuserFps.Where(x => /*(x.Datefrom >= Dfrom && x.Dateto <= Dto) &&*/ x.Userid == custid).ToList();
                var customer = _context.UserFps.Where(x => x.Userid == custid).ToList();
                var product = _context.ProductFps.ToList();
                var multi = from c in customer
                            join pc in customerproduct on c.Userid equals pc.Userid
                            join pp in product on pc.Productid equals pp.Productid

                            select new JoinTable { productFp = pp, userFp = c, productuserFp = pc };

                if (customerproduct != null)
                {
                    return View(multi);
                }
                else
                {
                    return View();
                }
                }
                
               
            }
            return View();
        }
        [HttpPost]
        public IActionResult Index(DateTime Dto ,DateTime Dfrom)
        {
            var custid = HttpContext.Session.GetInt32("CustomerId");
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            if (custid != null)
            {
                var item2 = _context.ProductuserFps.Where(x => x.Userid == custid).FirstOrDefault();

                var item3 = _context.ProductFps.Where(x => x.Productid == item2.Productid).FirstOrDefault();
                ViewBag.Userinfo = item2;
                ViewBag.Userinfo2 = item3;

                var customerproduct = _context.ProductuserFps.Where(x => /*(x.Datefrom >= Dfrom && x.Dateto <= Dto) &&*/ x.Userid == custid).ToList();
                var customer = _context.UserFps.Where(x => x.Userid == custid).ToList();
                var product = _context.ProductFps.ToList();
                var multi = from c in customer
                            join pc in customerproduct on c.Userid equals pc.Userid
                            join pp in product on pc.Productid equals pp.Productid

                            select new JoinTable { productFp = pp, userFp = c, productuserFp = pc };

                if (customerproduct != null)
                {
                    return View(multi);
                }
                else
                {
                    return View();
                }
            }
            return View();  
            


           
        }
        public IActionResult UserProfil()
        {
            
            var custid = HttpContext.Session.GetInt32("CustomerId");
            if (custid != null)
            {
                var item2 = _context.UserFps.Where(x => x.Userid == custid).FirstOrDefault();
                ViewBag.emailcustomer = HttpContext.Session.GetString("CustomerEmail");
                ViewBag.passwordcustomer = HttpContext.Session.GetString("CustomerPassword");
                ViewBag.Userinfo = item2;
                ViewBag.Userinfo3 = item2;

                UpdateProfile updateProfile = new UpdateProfile();

                return View(updateProfile);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UserProfil( UpdateProfile updateProfile)
        {
            var custid = HttpContext.Session.GetInt32("CustomerId");
            if (custid != null)
            {
                var item2 = _context.UserFps.Where(x => x.Userid == custid).FirstOrDefault();
                ViewBag.emailcustomer = HttpContext.Session.GetString("CustomerEmail");
                ViewBag.passwordcustomer = HttpContext.Session.GetString("CustomerPassword");
                ViewBag.Userinfo = item2;
                ViewBag.Userinfo3 = item2;

                var x = _context.UserFps.Where(x => x.Userid == custid).FirstOrDefault();
                var y = _context.UserloginFps.Where(x => x.Userid == custid).FirstOrDefault();
                if (updateProfile.FName != null)
                {
                    x.Fname = updateProfile.FName;

                }
                if (updateProfile.LName != null)
                {
                    x.Lname = updateProfile.LName;
                }
                if (updateProfile.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + updateProfile.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await updateProfile.ImageFile.CopyToAsync(fileStream);
                    }
                    x.Imagepath = fileName;
                    _context.UserFps.Update(x);
                    await _context.SaveChangesAsync();

                }

                _context.Update(x);
                await _context.SaveChangesAsync();
                if (updateProfile.Email != null)
                {
                    y.Email = updateProfile.Email;
                }
                _context.Update(y);
                await _context.SaveChangesAsync();
                if (updateProfile.Password != null)
                {
                    y.Password = updateProfile.Password;
                }
                _context.Update(y);
                await _context.SaveChangesAsync();



                return View(updateProfile);
            }
            else
            {
                return View();
            }
        }


    }
}

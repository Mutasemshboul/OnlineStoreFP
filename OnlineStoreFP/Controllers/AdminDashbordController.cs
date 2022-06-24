using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineStoreFP.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineStoreFP.Controllers
{
    public class AdminDashbordController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public AdminDashbordController(ModelContext context, IWebHostEnvironment _webHostEnviroment)
        {
            _context = context;
            this._webHostEnviroment = _webHostEnviroment;
        }
        public IActionResult Index()
        {
            var numuser = _context.UserFps.Count();
            var item2 = _context.ProductFps.Max(x=>x.Price);
            var numproducts = _context.ProductFps.Count();
            var numcategory = _context.CategoryFps.Count();
            var numstores = _context.Stores.Count();
            var nummessege = _context.ContactusFps.Count();
            
            ViewBag.maxprice = item2;
            ViewBag.noofcustomer = numuser-1;
            ViewBag.noofproducts = numproducts;
            ViewBag.noofcategory = numcategory;
            ViewBag.noofstores = numstores;
            ViewBag.noofmessege = nummessege;
            var getuser = _context.UserFps.ToList();
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            ViewBag.Idcustomer = HttpContext.Session.GetInt32("CustomerId");
            return View();

        }
        public IActionResult usersinformation()
        {
            var getuser = _context.UserFps.ToList();

            var customer = _context.UserFps.ToList();
            var userlgin = _context.UserloginFps.ToList();
            
            
            var multi = from c in customer
                        join us in userlgin on c.Userid equals us.Userid


                        select new JoinTable {  userFp = c ,userloginFp = us};
            var getall = Tuple.Create<IEnumerable<JoinTable>, IEnumerable<OnlineStoreFP.Models.UserFp>>(multi, getuser);
            return View(getall);
        }
        public IActionResult manageCategory()
        {
            var cat = _context.CategoryFps.ToList();
            var getall = Tuple.Create<IEnumerable<OnlineStoreFP.Models.CategoryFp>>(cat);
            

            return View(getall);
        }
        public IActionResult manageStores()
        {
            
            var product = _context.ProductFps.ToList();
            var store = _context.Stores.ToList();
            var category = _context.CategoryFps.ToList();
            
            var multi = from s in store
                        
                        join c in category on s.Categoryid equals c.Categoryid

                        select new JoinTable { store = s, categoryFp = c };
            var getall = Tuple.Create<IEnumerable<JoinTable>>(multi);
            return View(getall);
        }
        public IActionResult ManageProducts()
        {
            var category = _context.CategoryFps.ToList();
            var product = _context.ProductFps.ToList();
            var store = _context.Stores.ToList();
            var multi = from s in store
                        join p in product on s.Storeid equals p.Storeid
                        join c in category on s.Categoryid equals c.Categoryid
                        select new JoinTable { store = s,productFp = p,categoryFp=c };
            var getall = Tuple.Create<IEnumerable<JoinTable>>(multi);
            return View(getall);
        }
        public IActionResult ProductCustomer()
        {
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            var customerproduct = _context.ProductuserFps.ToList();
            var customer = _context.UserFps.ToList();
            var product = _context.ProductFps.ToList();
            var multi = from c in customer
                        join pc in customerproduct on c.Userid equals pc.Userid
                        join pp in product on pc.Productid equals pp.Productid

                        select new JoinTable { productFp = pp, userFp = c, productuserFp = pc };




            return View(multi);
            
        }
        [HttpPost]
        public IActionResult ProductCustomer(DateTime? Dfrom, DateTime? Dto)
        {
            var customerproduct = _context.ProductuserFps.ToList();
            var customer = _context.UserFps.ToList();
            var product = _context.ProductFps.ToList();
            var multi = from c in customer
                        join pc in customerproduct on c.Userid equals pc.Userid
                        join pp in product on pc.Productid equals pp.Productid

                        select new JoinTable { productFp = pp, userFp = c, productuserFp = pc };




            return View(multi);
        }
        public IActionResult test()
        {
            return View();
        }
        public IActionResult AdminProfile()
        {
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
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
        public async Task<IActionResult> AdminProfileAsync(UpdateProfile updateProfile)
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


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MimeKit;
using OnlineStoreFP.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace OnlineStoreFP.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;

        public HomeController(ILogger<HomeController> logger , ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}\
        public IActionResult Index()
        {
            var modelContext1 = _context.ProductFps.Include(p => p.Store.Category).ToList();
            var modelContext2 = _context.CategoryFps.ToList();
            var getall = Tuple.Create<IEnumerable<OnlineStoreFP.Models.ProductFp>, IEnumerable<OnlineStoreFP.Models.CategoryFp>>(modelContext1, modelContext2);
            var custId = HttpContext.Session.GetInt32("CustomerId");
            //ShoppingCart shoppingCart = new ShoppingCart();
            //shoppingCart.Userid = custId;
            //shoppingCart.Productid = 61;

            //_context.Add(shoppingCart);
            return View(getall);
        }
        [HttpPost]
        public async Task<IActionResult> Index( ShoppingCart shoppingCart , int proid)
        {
            var modelContext1 = _context.ProductFps.Include(p => p.Store.Category).ToList();
            var modelContext2 = _context.CategoryFps.ToList();
            var getall = Tuple.Create<IEnumerable<OnlineStoreFP.Models.ProductFp>, IEnumerable<OnlineStoreFP.Models.CategoryFp>>(modelContext1, modelContext2);


            
            //var getall = Tuple.Create<IEnumerable<OnlineStoreFP.Models.ProductFp>, IEnumerable<OnlineStoreFP.Models.CategoryFp>>(modelContext1, modelContext2);
            
            var custId = HttpContext.Session.GetInt32("CustomerId");
            if(custId != null)
            {
                shoppingCart.Userid = custId;
                shoppingCart.Productid = proid;
                _context.Add(shoppingCart);

            }
            
            await _context.SaveChangesAsync();
            //shoppingCart.Productid = proiud;
             return View(getall);
            
        }
        public async Task<IActionResult> ShoppingCart()
        {
            //ViewBag.Idcustomer = HttpContext.Session.GetInt32("CustomerId");
            var custId = HttpContext.Session.GetInt32("CustomerId");
            var modelContext = _context.ShoppingCarts.Where(x=> x.User.Userid==custId).Include(s => s.Product).Include(s => s.User);
            return View(await modelContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> ShoppingCart(decimal catrId)
        {
            var shoppingCart = await _context.ShoppingCarts.FindAsync(catrId);
            //shoppingCart.Userid = custId;
            _context.ShoppingCarts.Remove(shoppingCart);
            await _context.SaveChangesAsync();

            return View();
        }


        [HttpGet]
        public async Task<IActionResult> CheckOut()
        {

            var custId = HttpContext.Session.GetInt32("CustomerId");
            var modelContext = _context.ShoppingCarts.Where(x => x.User.Userid == custId).Include(s => s.Product).Include(s => s.User);
            
            
            //await _context.SaveChangesAsync();
            return View(await modelContext.ToListAsync());
        }
        
        public async Task<IActionResult> CheckOuta()
        {
            var custId = HttpContext.Session.GetInt32("CustomerId");
            var modelContext = _context.ShoppingCarts.Where(x => x.User.Userid == custId).Include(s => s.Product).Include(s => s.User).ToList();
            var eamilcust = _context.UserloginFps.Where(x => x.User.Userid == custId).FirstOrDefault();
            


            try
            {
                if (custId != null)
                {

                    using SmtpClient mySmtpClient = new SmtpClient("smtp.office365.com", 587);
                    mySmtpClient.EnableSsl = true;

                    // set smtp-client with basicAuthentication
                    mySmtpClient.UseDefaultCredentials = false;
                    NetworkCredential basicAuthenticationInfo = new
                      NetworkCredential("mutasemshboul@outlook.com", "mutasem12345");
                    mySmtpClient.Credentials = basicAuthenticationInfo;

                    // add from,to mailaddresses
                    //use your email
                    MailAddress from = new MailAddress("mutasemshboul@outlook.com", "Market Store Admin");

                    //Send mail to:
                    //use customer email
                    MailboxAddress emailto = new MailboxAddress("user", eamilcust.Email);
                    MailAddress to = new MailAddress(eamilcust.Email, "Customer:");
                    using MailMessage myMail = new MailMessage(from, to);



                    // set subject and encoding
                    myMail.Subject = "Thank you for shopping";
                    myMail.SubjectEncoding = Encoding.UTF8;

                    // set body-message and encoding
                    myMail.Body = "<h1>Hello</h1><h3>Purchase completed successfully</h3>";
                    myMail.BodyEncoding = Encoding.UTF8;
                    // text or html
                    myMail.IsBodyHtml = true;

                    await mySmtpClient.SendMailAsync(myMail);
                    Console.WriteLine("Email Has Been Send");
                }

            }

            catch (SmtpException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            if (custId != null)
            {
                ProductuserFp productuserFp = new ProductuserFp();
                productuserFp.Userid = custId;
                int lastId = (int)_context.ProductuserFps.OrderByDescending(p => p.Productuserid).FirstOrDefault().Productuserid;
                foreach (var id in modelContext)
                {
                    productuserFp.Productuserid = ++lastId;

                    productuserFp.Productid = id.Productid;
                    productuserFp.Quantity = id.Quantity;
                    
                    _context.ProductuserFps.Add(productuserFp);
                    await _context.SaveChangesAsync();
                }

                var s = _context.ShoppingCarts.Where(x => x.Userid == custId).ToList();
                foreach (var item in s)
                {
                    var shoppingCart = await _context.ShoppingCarts.FindAsync(item.Id);
                    //shoppingCart.Userid = custId;
                    _context.ShoppingCarts.Remove(shoppingCart);
                    await _context.SaveChangesAsync();

                }
            }

            return RedirectToAction("Index");

        }
        //[HttpPost]
        //public async Task<IActionResult> DeleteCarts(decimal catrId)
        //{
        //    var shoppingCart = await _context.ShoppingCarts.FindAsync(catrId);
        //    //shoppingCart.Userid = custId;
        //    _context.ShoppingCarts.Remove(shoppingCart);
        //    await _context.SaveChangesAsync();

        //    return View();
        //}
        
        public async Task<JsonResult> AddCart( int proid)
        {
            ShoppingCart shoppingCart =new ShoppingCart();  
            var custId = HttpContext.Session.GetInt32("CustomerId");
            if (custId != null)
            {
                shoppingCart.Userid = custId;
                shoppingCart.Productid = proid;
                _context.Add(shoppingCart);
                await _context.SaveChangesAsync();
                return Json(1);

            }

            return Json(0);
        }

        public async Task<IActionResult> ContactUs() 
        {
            //var contactus = _context.ContactusFps.ToListAsync(); 
            //var contactusDynmic = _context.Contactdynmics.ToListAsync();
            //var getall = Tuple.Create<OnlineStoreFP.Models.ContactusFp,OnlineStoreFP.Models.Contactdynmic>(contactus, contactusDynmic);
            //return View(getall);
            return View(await _context.Contactdynmics.ToListAsync());

        }
        public async Task<IActionResult> Shops()
        {
            var modelContext = _context.ProductFps.Include(p => p.Store.Category);
            return View(await modelContext.ToListAsync());
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
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

        public async Task<JsonResult> UpdataQty(int id, int qty)
        {

            var cart = _context.ShoppingCarts.Find((decimal)id);

            cart.Quantity= qty;
            _context.ShoppingCarts.Update(cart);

            await _context.SaveChangesAsync();
           
            return Json(cart);
        }
    }
}

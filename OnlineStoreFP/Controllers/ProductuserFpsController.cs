using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStoreFP.Models;

namespace OnlineStoreFP.Controllers
{
    public class ProductuserFpsController : Controller
    {
        private readonly ModelContext _context;

        public ProductuserFpsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ProductuserFps
        public async Task<IActionResult> Index()
        {
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            var modelContext = _context.ProductuserFps.Include(p => p.Product).Include(p => p.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductuserFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productuserFp = await _context.ProductuserFps
                .Include(p => p.Product)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Productuserid == id);
            if (productuserFp == null)
            {
                return NotFound();
            }

            return View(productuserFp);
        }

        // GET: ProductuserFps/Create
        public IActionResult Create()
        {
            ViewData["Productid"] = new SelectList(_context.ProductFps, "Productid", "Productid");
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid");
            return View();
        }

        // POST: ProductuserFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Productuserid,Datefrom,Dateto,Quantity,Productid,Userid")] ProductuserFp productuserFp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productuserFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.ProductFps, "Productid", "Productid", productuserFp.Productid);
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", productuserFp.Userid);
            return View(productuserFp);
        }

        // GET: ProductuserFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productuserFp = await _context.ProductuserFps.FindAsync(id);
            if (productuserFp == null)
            {
                return NotFound();
            }
            ViewData["Productid"] = new SelectList(_context.ProductFps, "Productid", "Productid", productuserFp.Productid);
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", productuserFp.Userid);
            return View(productuserFp);
        }

        // POST: ProductuserFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Productuserid,Datefrom,Dateto,Quantity,Productid,Userid")] ProductuserFp productuserFp)
        {
            if (id != productuserFp.Productuserid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(productuserFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductuserFpExists(productuserFp.Productuserid))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Productid"] = new SelectList(_context.ProductFps, "Productid", "Productid", productuserFp.Productid);
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", productuserFp.Userid);
            return View(productuserFp);
        }

        // GET: ProductuserFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productuserFp = await _context.ProductuserFps
                .Include(p => p.Product)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Productuserid == id);
            if (productuserFp == null)
            {
                return NotFound();
            }

            return View(productuserFp);
        }

        // POST: ProductuserFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productuserFp = await _context.ProductuserFps.FindAsync(id);
            _context.ProductuserFps.Remove(productuserFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductuserFpExists(decimal id)
        {
            return _context.ProductuserFps.Any(e => e.Productuserid == id);
        }
    }
}

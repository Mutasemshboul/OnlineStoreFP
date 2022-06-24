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
    public class ProductFpsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public ProductFpsController(ModelContext context, IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;

        }

        // GET: ProductFps
        public async Task<IActionResult> Index()
        {
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            var modelContext = _context.ProductFps.Include(p => p.Store.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFp = await _context.ProductFps
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (productFp == null)
            {
                return NotFound();
            }

            return View(productFp);
        }

        // GET: ProductFps/Create
        public IActionResult Create()
        {
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storename");
            return View();
        }

        // POST: ProductFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductFp productFp)
        {
            if (ModelState.IsValid)
            {
                if (productFp.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + productFp.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await productFp.ImageFile.CopyToAsync(fileStream);
                    }
                    productFp.Imagepath = fileName;
                    _context.Add(productFp);
                }
                _context.Add(productFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storeid", productFp.Storeid);
            //ViewData["Storename"] = new SelectList(_context.Stores)
            return View(productFp);
        }

        // GET: ProductFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFp = await _context.ProductFps.FindAsync(id);
            if (productFp == null)
            {
                return NotFound();
            }
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storename", productFp.Storeid);
            return View(productFp);
        }

        // POST: ProductFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  ProductFp productFp)
        {
            if (id != productFp.Productid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                 try
                    {
               
                    if (productFp.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + productFp.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await productFp.ImageFile.CopyToAsync(fileStream);
                        }
                        productFp.Imagepath = fileName;
                    }
                    else
                    {
                        productFp.Imagepath = (from c in _context.ProductFps
                                              where c.Productid.Equals(id)
                                              select c.Imagepath).FirstOrDefault();
                    }

                    _context.Update(productFp);
                    await _context.SaveChangesAsync();
                    }

                
                     catch (DbUpdateConcurrencyException)
                     {
                    if (!ProductFpExists(productFp.Productid))
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
            ViewData["Storeid"] = new SelectList(_context.Stores, "Storeid", "Storeid", productFp.Storeid);
            return View(productFp);
        }
        public async Task<IActionResult> Try()
        {
            var modelContext = _context.ProductFps.Include(p => p.Store.Category);
            return View(await modelContext.ToListAsync());
        }

        // GET: ProductFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productFp = await _context.ProductFps
                .Include(p => p.Store)
                .FirstOrDefaultAsync(m => m.Productid == id);
            if (productFp == null)
            {
                return NotFound();
            }

            return View(productFp);
        }

        // POST: ProductFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var productFp = await _context.ProductFps.FindAsync(id);
            _context.ProductFps.Remove(productFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductFpExists(decimal id)
        {
            return _context.ProductFps.Any(e => e.Productid == id);
        }
    }
}

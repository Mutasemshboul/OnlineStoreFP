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
    public class CategoryFpsController : Controller
    {
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment _webHostEnviroment;

        public CategoryFpsController(ModelContext context , IWebHostEnvironment webHostEnviroment)
        {
            _context = context;
            _webHostEnviroment = webHostEnviroment;
        }

        // GET: CategoryFps
        public async Task<IActionResult> Index()
        {

            


            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            return View(await _context.CategoryFps.ToListAsync());
        }

        // GET: CategoryFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFp = await _context.CategoryFps
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (categoryFp == null)
            {
                return NotFound();
            }

            return View(categoryFp);
        }

        // GET: CategoryFps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoryFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( CategoryFp categoryFp)
        {
            if (ModelState.IsValid)
            {
                if (categoryFp.ImageFile != null)
                {
                    string wwwRootPath = _webHostEnviroment.WebRootPath;
                    string fileName = Guid.NewGuid().ToString() + "_" + categoryFp.ImageFile.FileName;
                    string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await categoryFp.ImageFile.CopyToAsync(fileStream);
                    }
                    categoryFp.Imagepath = fileName;
                    _context.Add(categoryFp);
                }
                _context.Add(categoryFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(categoryFp);
        }

        // GET: CategoryFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFp = await _context.CategoryFps.FindAsync(id);
            if (categoryFp == null)
            {
                return NotFound();
            }
            return View(categoryFp);
        }

        // POST: CategoryFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id,  CategoryFp categoryFp)
        {
            if (id != categoryFp.Categoryid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (categoryFp.ImageFile != null)
                    {
                        string wwwRootPath = _webHostEnviroment.WebRootPath;
                        string fileName = Guid.NewGuid().ToString() + "_" + categoryFp.ImageFile.FileName;
                        string path = Path.Combine(wwwRootPath + "/Image/", fileName);
                        using (var fileStream = new FileStream(path, FileMode.Create))
                        {
                            await categoryFp.ImageFile.CopyToAsync(fileStream);
                        }
                        categoryFp.Imagepath = fileName;
                       
                    }
                else
                {
                    categoryFp.Imagepath = (from c in _context.CategoryFps
                                           where c.Categoryid.Equals(id)
                                           select c.Imagepath).FirstOrDefault();
                }

                    _context.Update(categoryFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryFpExists(categoryFp.Categoryid))
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
            return View(categoryFp);
        }

        // GET: CategoryFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoryFp = await _context.CategoryFps
                .FirstOrDefaultAsync(m => m.Categoryid == id);
            if (categoryFp == null)
            {
                return NotFound();
            }

            return View(categoryFp);
        }

        // POST: CategoryFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var categoryFp = await _context.CategoryFps.FindAsync(id);
            _context.CategoryFps.Remove(categoryFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryFpExists(decimal id)
        {
            return _context.CategoryFps.Any(e => e.Categoryid == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineStoreFP.Models;

namespace OnlineStoreFP.Controllers
{
    public class TestimonialFpsController : Controller
    {
        private readonly ModelContext _context;

        public TestimonialFpsController(ModelContext context)
        {
            _context = context;
        }

        // GET: TestimonialFps
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.TestimonialFps.Include(t => t.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: TestimonialFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonialFp = await _context.TestimonialFps
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (testimonialFp == null)
            {
                return NotFound();
            }

            return View(testimonialFp);
        }

        // GET: TestimonialFps/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid");
            return View();
        }

        // POST: TestimonialFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Testimonialid,Message,Userid")] TestimonialFp testimonialFp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(testimonialFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", testimonialFp.Userid);
            return View(testimonialFp);
        }

        // GET: TestimonialFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonialFp = await _context.TestimonialFps.FindAsync(id);
            if (testimonialFp == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", testimonialFp.Userid);
            return View(testimonialFp);
        }

        // POST: TestimonialFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Testimonialid,Message,Userid")] TestimonialFp testimonialFp)
        {
            if (id != testimonialFp.Testimonialid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(testimonialFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TestimonialFpExists(testimonialFp.Testimonialid))
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
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", testimonialFp.Userid);
            return View(testimonialFp);
        }

        // GET: TestimonialFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var testimonialFp = await _context.TestimonialFps
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Testimonialid == id);
            if (testimonialFp == null)
            {
                return NotFound();
            }

            return View(testimonialFp);
        }

        // POST: TestimonialFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var testimonialFp = await _context.TestimonialFps.FindAsync(id);
            _context.TestimonialFps.Remove(testimonialFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TestimonialFpExists(decimal id)
        {
            return _context.TestimonialFps.Any(e => e.Testimonialid == id);
        }
    }
}

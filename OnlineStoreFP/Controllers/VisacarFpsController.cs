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
    public class VisacarFpsController : Controller
    {
        private readonly ModelContext _context;

        public VisacarFpsController(ModelContext context)
        {
            _context = context;
        }

        // GET: VisacarFps
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.VisacarFps.Include(v => v.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: VisacarFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visacarFp = await _context.VisacarFps
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Visaid == id);
            if (visacarFp == null)
            {
                return NotFound();
            }

            return View(visacarFp);
        }

        // GET: VisacarFps/Create
        public IActionResult Create()
        {
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid");
            return View();
        }

        // POST: VisacarFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Visaid,Numbercard,Balance,Userid")] VisacarFp visacarFp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(visacarFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", visacarFp.Userid);
            return View(visacarFp);
        }

        // GET: VisacarFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visacarFp = await _context.VisacarFps.FindAsync(id);
            if (visacarFp == null)
            {
                return NotFound();
            }
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", visacarFp.Userid);
            return View(visacarFp);
        }

        // POST: VisacarFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Visaid,Numbercard,Balance,Userid")] VisacarFp visacarFp)
        {
            if (id != visacarFp.Visaid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(visacarFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VisacarFpExists(visacarFp.Visaid))
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
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", visacarFp.Userid);
            return View(visacarFp);
        }

        // GET: VisacarFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var visacarFp = await _context.VisacarFps
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Visaid == id);
            if (visacarFp == null)
            {
                return NotFound();
            }

            return View(visacarFp);
        }

        // POST: VisacarFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var visacarFp = await _context.VisacarFps.FindAsync(id);
            _context.VisacarFps.Remove(visacarFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VisacarFpExists(decimal id)
        {
            return _context.VisacarFps.Any(e => e.Visaid == id);
        }
    }
}

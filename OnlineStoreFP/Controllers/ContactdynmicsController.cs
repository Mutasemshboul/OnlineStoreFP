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
    public class ContactdynmicsController : Controller
    {
        private readonly ModelContext _context;

        public ContactdynmicsController(ModelContext context)
        {
            _context = context;
        }

        // GET: Contactdynmics
        public async Task<IActionResult> Index()
        {
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            return View(await _context.Contactdynmics.ToListAsync());
        }

        // GET: Contactdynmics/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactdynmic = await _context.Contactdynmics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactdynmic == null)
            {
                return NotFound();
            }

            return View(contactdynmic);
        }

        // GET: Contactdynmics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Contactdynmics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Information,Contactus,Description,State1,Street1,Phone1,State2,Street2,Phone2")] Contactdynmic contactdynmic)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactdynmic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(contactdynmic);
        }

        // GET: Contactdynmics/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactdynmic = await _context.Contactdynmics.FindAsync(id);
            if (contactdynmic == null)
            {
                return NotFound();
            }
            return View(contactdynmic);
        }

        // POST: Contactdynmics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Id,Information,Contactus,Description,State1,Street1,Phone1,State2,Street2,Phone2")] Contactdynmic contactdynmic)
        {
            if (id != contactdynmic.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactdynmic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactdynmicExists(contactdynmic.Id))
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
            return View(contactdynmic);
        }

        // GET: Contactdynmics/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactdynmic = await _context.Contactdynmics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (contactdynmic == null)
            {
                return NotFound();
            }

            return View(contactdynmic);
        }

        // POST: Contactdynmics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactdynmic = await _context.Contactdynmics.FindAsync(id);
            _context.Contactdynmics.Remove(contactdynmic);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactdynmicExists(decimal id)
        {
            return _context.Contactdynmics.Any(e => e.Id == id);
        }
    }
}

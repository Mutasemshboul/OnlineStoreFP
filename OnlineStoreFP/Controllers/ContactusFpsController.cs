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
    public class ContactusFpsController : Controller
    {
        private readonly ModelContext _context;

        public ContactusFpsController(ModelContext context)
        {
            _context = context;
        }

        // GET: ContactusFps
        public async Task<IActionResult> Index()
        {
            
            ViewBag.namecustomer = HttpContext.Session.GetString("CustomerName");
            return View(await _context.ContactusFps.ToListAsync());
            
        }

        // GET: ContactusFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactusFp = await _context.ContactusFps
                .FirstOrDefaultAsync(m => m.Contactid == id);
            if (contactusFp == null)
            {
                return NotFound();
            }

            return View(contactusFp);
        }

        // GET: ContactusFps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ContactusFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Email,Subject,Message")] ContactusFp contactusFp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactusFp);
                await _context.SaveChangesAsync();
                return RedirectToAction("ContactUs", "Home"); 
            }
            return View(contactusFp);
        }

        // GET: ContactusFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactusFp = await _context.ContactusFps.FindAsync(id);
            if (contactusFp == null)
            {
                return NotFound();
            }
            return View(contactusFp);
        }

        // POST: ContactusFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Contactid,Name,Email,Subject,Message")] ContactusFp contactusFp)
        {
            if (id != contactusFp.Contactid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactusFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactusFpExists(contactusFp.Contactid))
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
            return View(contactusFp);
        }

        // GET: ContactusFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactusFp = await _context.ContactusFps
                .FirstOrDefaultAsync(m => m.Contactid == id);
            if (contactusFp == null)
            {
                return NotFound();
            }

            return View(contactusFp);
        }

        // POST: ContactusFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var contactusFp = await _context.ContactusFps.FindAsync(id);
            _context.ContactusFps.Remove(contactusFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactusFpExists(decimal id)
        {
            return _context.ContactusFps.Any(e => e.Contactid == id);
        }
    }
}

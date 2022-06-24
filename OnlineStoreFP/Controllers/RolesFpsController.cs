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
    public class RolesFpsController : Controller
    {
        private readonly ModelContext _context;

        public RolesFpsController(ModelContext context)
        {
            _context = context;
        }

        // GET: RolesFps
        public async Task<IActionResult> Index()
        {
            return View(await _context.RolesFps.ToListAsync());
        }

        // GET: RolesFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolesFp = await _context.RolesFps
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (rolesFp == null)
            {
                return NotFound();
            }

            return View(rolesFp);
        }

        // GET: RolesFps/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: RolesFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Roleid,Rolename")] RolesFp rolesFp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rolesFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rolesFp);
        }

        // GET: RolesFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolesFp = await _context.RolesFps.FindAsync(id);
            if (rolesFp == null)
            {
                return NotFound();
            }
            return View(rolesFp);
        }

        // POST: RolesFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Roleid,Rolename")] RolesFp rolesFp)
        {
            if (id != rolesFp.Roleid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rolesFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RolesFpExists(rolesFp.Roleid))
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
            return View(rolesFp);
        }

        // GET: RolesFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rolesFp = await _context.RolesFps
                .FirstOrDefaultAsync(m => m.Roleid == id);
            if (rolesFp == null)
            {
                return NotFound();
            }

            return View(rolesFp);
        }

        // POST: RolesFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var rolesFp = await _context.RolesFps.FindAsync(id);
            _context.RolesFps.Remove(rolesFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RolesFpExists(decimal id)
        {
            return _context.RolesFps.Any(e => e.Roleid == id);
        }
    }
}

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
    public class UserloginFpsController : Controller
    {
        private readonly ModelContext _context;

        public UserloginFpsController(ModelContext context)
        {
            _context = context;
        }

        // GET: UserloginFps
        public async Task<IActionResult> Index()
        {
            var modelContext = _context.UserloginFps.Include(u => u.Role).Include(u => u.User);
            return View(await modelContext.ToListAsync());
        }

        // GET: UserloginFps/Details/5
        public async Task<IActionResult> Details(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userloginFp = await _context.UserloginFps
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Loginid == id);
            if (userloginFp == null)
            {
                return NotFound();
            }

            return View(userloginFp);
        }

        // GET: UserloginFps/Create
        public IActionResult Create()
        {
            ViewData["Roleid"] = new SelectList(_context.RolesFps, "Roleid", "Roleid");
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid");
            return View();
        }

        // POST: UserloginFps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Loginid,Email,Password,Roleid,Userid")] UserloginFp userloginFp)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userloginFp);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Roleid"] = new SelectList(_context.RolesFps, "Roleid", "Roleid", userloginFp.Roleid);
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", userloginFp.Userid);
            return View(userloginFp);
        }

        // GET: UserloginFps/Edit/5
        public async Task<IActionResult> Edit(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userloginFp = await _context.UserloginFps.FindAsync(id);
            if (userloginFp == null)
            {
                return NotFound();
            }
            ViewData["Roleid"] = new SelectList(_context.RolesFps, "Roleid", "Roleid", userloginFp.Roleid);
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", userloginFp.Userid);
            return View(userloginFp);
        }

        // POST: UserloginFps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(decimal id, [Bind("Loginid,Email,Password,Roleid,Userid")] UserloginFp userloginFp)
        {
            if (id != userloginFp.Loginid)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userloginFp);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserloginFpExists(userloginFp.Loginid))
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
            ViewData["Roleid"] = new SelectList(_context.RolesFps, "Roleid", "Roleid", userloginFp.Roleid);
            ViewData["Userid"] = new SelectList(_context.UserFps, "Userid", "Userid", userloginFp.Userid);
            return View(userloginFp);
        }

        // GET: UserloginFps/Delete/5
        public async Task<IActionResult> Delete(decimal? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userloginFp = await _context.UserloginFps
                .Include(u => u.Role)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.Loginid == id);
            if (userloginFp == null)
            {
                return NotFound();
            }

            return View(userloginFp);
        }

        // POST: UserloginFps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(decimal id)
        {
            var userloginFp = await _context.UserloginFps.FindAsync(id);
            _context.UserloginFps.Remove(userloginFp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserloginFpExists(decimal id)
        {
            return _context.UserloginFps.Any(e => e.Loginid == id);
        }
    }
}

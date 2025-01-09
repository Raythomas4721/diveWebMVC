using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diveWebMVC.Models;

namespace diveWebMVC.Controllers
{
    public class TMadminsController : Controller
    {
        private readonly diveShopperContext _context;

        public TMadminsController(diveShopperContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetAdmin()
        {

            var TMadmin = _context.TMadmins.Select(admin => new
            {
                admin.AdminId,
                admin.UserName,
                admin.Email,
                admin.RoleName,
                admin.CreateAt,
                admin.LastLogin
            });

            // 返回 JSON 格式的資料
            return Json(TMadmin);
        }


        // GET: TMadmins
        public async Task<IActionResult> Index()
        {
            return View(await _context.TMadmins.ToListAsync());
        }

        // GET: TMadmins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMadmin = await _context.TMadmins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (tMadmin == null)
            {
                return NotFound();
            }

            return View(tMadmin);
        }

        // GET: TMadmins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TMadmins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AdminId,UserName,PasswordHash,Email,RoleName,CreateAt,LastLogin")] TMadmin tMadmin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tMadmin);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tMadmin);
        }

        // GET: TMadmins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMadmin = await _context.TMadmins.FindAsync(id);
            if (tMadmin == null)
            {
                return NotFound();
            }
            return View(tMadmin);
        }

        // POST: TMadmins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AdminId,UserName,PasswordHash,Email,RoleName,CreateAt,LastLogin")] TMadmin tMadmin)
        {
            if (id != tMadmin.AdminId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tMadmin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TMadminExists(tMadmin.AdminId))
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
            return View(tMadmin);
        }

        // GET: TMadmins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMadmin = await _context.TMadmins
                .FirstOrDefaultAsync(m => m.AdminId == id);
            if (tMadmin == null)
            {
                return NotFound();
            }

            return View(tMadmin);
        }

        // POST: TMadmins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tMadmin = await _context.TMadmins.FindAsync(id);
            if (tMadmin != null)
            {
                _context.TMadmins.Remove(tMadmin);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TMadminExists(int id)
        {
            return _context.TMadmins.Any(e => e.AdminId == id);
        }
    }
}

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
    public class TNordersController : Controller
    {
        private readonly diveShopperContext _context;

        public TNordersController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TNorders
        public async Task<IActionResult> Index()
        {
            var diveShopperContext = _context.TNorders.Include(t => t.Member);
            return View(await diveShopperContext.ToListAsync());
        }

        // GET: TNorders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNorder = await _context.TNorders
                .Include(t => t.Member)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tNorder == null)
            {
                return NotFound();
            }

            return View(tNorder);
        }

        // GET: TNorders/Create
        public IActionResult Create()
        {
            ViewData["MemberId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId");
            return View();
        }

        // POST: TNorders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,MemberId,OrderDate,Address,OrderStatus,PaymentStatus,TotalAmount,CreatedDate")] TNorder tNorder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNorder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MemberId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId", tNorder.MemberId);
            return View(tNorder);
        }

        // GET: TNorders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNorder = await _context.TNorders.FindAsync(id);
            if (tNorder == null)
            {
                return NotFound();
            }
            ViewData["MemberId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId", tNorder.MemberId);
            return View(tNorder);
        }

        // POST: TNorders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,MemberId,OrderDate,Address,OrderStatus,PaymentStatus,TotalAmount,CreatedDate")] TNorder tNorder)
        {
            if (id != tNorder.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNorder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNorderExists(tNorder.OrderId))
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
            ViewData["MemberId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId", tNorder.MemberId);
            return View(tNorder);
        }

        // GET: TNorders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNorder = await _context.TNorders
                .Include(t => t.Member)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (tNorder == null)
            {
                return NotFound();
            }

            return View(tNorder);
        }

        // POST: TNorders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNorder = await _context.TNorders.FindAsync(id);
            if (tNorder != null)
            {
                _context.TNorders.Remove(tNorder);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNorderExists(int id)
        {
            return _context.TNorders.Any(e => e.OrderId == id);
        }
    }
}

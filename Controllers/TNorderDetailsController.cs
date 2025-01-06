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
    public class TNorderDetailsController : Controller
    {
        private readonly diveShopperContext _context;

        public TNorderDetailsController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TNorderDetails
        public async Task<IActionResult> Index()
        {
            var diveShopperContext = _context.TNorderDetails.Include(t => t.Order).Include(t => t.Productvariants);
            return View(diveShopperContext);
        }

        // GET: TNorderDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNorderDetail = await _context.TNorderDetails
                .Include(t => t.Order)
                .Include(t => t.Productvariants)
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (tNorderDetail == null)
            {
                return NotFound();
            }

            return View(tNorderDetail);
        }

        // GET: TNorderDetails/Create
        public IActionResult Create()
        {
            ViewData["OrderId"] = new SelectList(_context.TNorders, "OrderId", "OrderId");
            ViewData["ProductvariantsId"] = new SelectList(_context.TNproductvariants, "ProductvariantsId", "ProductvariantsId");
            return View();
        }

        // POST: TNorderDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderDetailsId,OrderId,ProductvariantsId,Quantity,UnitPrIce,TotalPrice")] TNorderDetail tNorderDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNorderDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderId"] = new SelectList(_context.TNorders, "OrderId", "OrderId", tNorderDetail.OrderId);
            ViewData["ProductvariantsId"] = new SelectList(_context.TNproductvariants, "ProductvariantsId", "ProductvariantsId", tNorderDetail.ProductvariantsId);
            return View(tNorderDetail);
        }

        // GET: TNorderDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNorderDetail = await _context.TNorderDetails.FindAsync(id);
            if (tNorderDetail == null)
            {
                return NotFound();
            }
            ViewData["OrderId"] = new SelectList(_context.TNorders, "OrderId", "OrderId", tNorderDetail.OrderId);
            ViewData["ProductvariantsId"] = new SelectList(_context.TNproductvariants, "ProductvariantsId", "ProductvariantsId", tNorderDetail.ProductvariantsId);
            return View(tNorderDetail);
        }

        // POST: TNorderDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderDetailsId,OrderId,ProductvariantsId,Quantity,UnitPrIce,TotalPrice")] TNorderDetail tNorderDetail)
        {
            if (id != tNorderDetail.OrderDetailsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNorderDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNorderDetailExists(tNorderDetail.OrderDetailsId))
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
            ViewData["OrderId"] = new SelectList(_context.TNorders, "OrderId", "OrderId", tNorderDetail.OrderId);
            ViewData["ProductvariantsId"] = new SelectList(_context.TNproductvariants, "ProductvariantsId", "ProductvariantsId", tNorderDetail.ProductvariantsId);
            return View(tNorderDetail);
        }

        // GET: TNorderDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNorderDetail = await _context.TNorderDetails
                .Include(t => t.Order)
                .Include(t => t.Productvariants)
                .FirstOrDefaultAsync(m => m.OrderDetailsId == id);
            if (tNorderDetail == null)
            {
                return NotFound();
            }

            return View(tNorderDetail);
        }

        // POST: TNorderDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNorderDetail = await _context.TNorderDetails.FindAsync(id);
            if (tNorderDetail != null)
            {
                _context.TNorderDetails.Remove(tNorderDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNorderDetailExists(int id)
        {
            return _context.TNorderDetails.Any(e => e.OrderDetailsId == id);
        }
    }
}

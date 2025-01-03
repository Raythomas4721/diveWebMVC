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
    public class TNproductsController : Controller
    {
        private readonly diveShopperContext _context;

        public TNproductsController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TNproducts
        public async Task<IActionResult> Index()
        {
            return View(await _context.TNproducts.ToListAsync());
        }

        // GET: TNproducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproduct = await _context.TNproducts
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tNproduct == null)
            {
                return NotFound();
            }

            return View(tNproduct);
        }

        // GET: TNproducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TNproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,UnitCost,Description,Picture")] TNproduct tNproduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tNproduct);
        }

        // GET: TNproducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproduct = await _context.TNproducts.FindAsync(id);
            if (tNproduct == null)
            {
                return NotFound();
            }
            return View(tNproduct);
        }

        // POST: TNproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,UnitCost,Description,Picture")] TNproduct tNproduct)
        {
            if (id != tNproduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNproductExists(tNproduct.ProductId))
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
            return View(tNproduct);
        }

        // GET: TNproducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproduct = await _context.TNproducts
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tNproduct == null)
            {
                return NotFound();
            }

            return View(tNproduct);
        }

        // POST: TNproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNproduct = await _context.TNproducts.FindAsync(id);
            if (tNproduct != null)
            {
                _context.TNproducts.Remove(tNproduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNproductExists(int id)
        {
            return _context.TNproducts.Any(e => e.ProductId == id);
        }
    }
}

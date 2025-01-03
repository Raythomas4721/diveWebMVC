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
    public class TUproductsController : Controller
    {
        private readonly diveShopperContext _context;

        public TUproductsController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TUproducts
        public async Task<IActionResult> Index()
        {
            var diveShopperContext = _context.TUproducts.Include(t => t.Category).Include(t => t.ProductCondition).Include(t => t.Seller);
            return View(await diveShopperContext.ToListAsync());
        }

        // GET: TUproducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUproduct = await _context.TUproducts
                .Include(t => t.Category)
                .Include(t => t.ProductCondition)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tUproduct == null)
            {
                return NotFound();
            }

            return View(tUproduct);
        }

        // GET: TUproducts/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.TUcategories, "CategoryId", "CategoryId");
            ViewData["ProductConditionId"] = new SelectList(_context.TUproductConditions, "ProductConditionId", "ProductConditionId");
            ViewData["SellerId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId");
            return View();
        }

        // POST: TUproducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,SellerId,CategoryId,ProductName,ProductDescription,ProductPrice,UpdatedAt,CreatedAt,ProductConditionId,ProductStatus")] TUproduct tUproduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tUproduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.TUcategories, "CategoryId", "CategoryId", tUproduct.CategoryId);
            ViewData["ProductConditionId"] = new SelectList(_context.TUproductConditions, "ProductConditionId", "ProductConditionId", tUproduct.ProductConditionId);
            ViewData["SellerId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId", tUproduct.SellerId);
            return View(tUproduct);
        }

        // GET: TUproducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUproduct = await _context.TUproducts.FindAsync(id);
            if (tUproduct == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.TUcategories, "CategoryId", "CategoryId", tUproduct.CategoryId);
            ViewData["ProductConditionId"] = new SelectList(_context.TUproductConditions, "ProductConditionId", "ProductConditionId", tUproduct.ProductConditionId);
            ViewData["SellerId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId", tUproduct.SellerId);
            return View(tUproduct);
        }

        // POST: TUproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,SellerId,CategoryId,ProductName,ProductDescription,ProductPrice,UpdatedAt,CreatedAt,ProductConditionId,ProductStatus")] TUproduct tUproduct)
        {
            if (id != tUproduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tUproduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TUproductExists(tUproduct.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.TUcategories, "CategoryId", "CategoryId", tUproduct.CategoryId);
            ViewData["ProductConditionId"] = new SelectList(_context.TUproductConditions, "ProductConditionId", "ProductConditionId", tUproduct.ProductConditionId);
            ViewData["SellerId"] = new SelectList(_context.TMmemberLists, "MemberId", "MemberId", tUproduct.SellerId);
            return View(tUproduct);
        }

        // GET: TUproducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tUproduct = await _context.TUproducts
                .Include(t => t.Category)
                .Include(t => t.ProductCondition)
                .Include(t => t.Seller)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (tUproduct == null)
            {
                return NotFound();
            }

            return View(tUproduct);
        }

        // POST: TUproducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tUproduct = await _context.TUproducts.FindAsync(id);
            if (tUproduct != null)
            {
                _context.TUproducts.Remove(tUproduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TUproductExists(int id)
        {
            return _context.TUproducts.Any(e => e.ProductId == id);
        }
    }
}

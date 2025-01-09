using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diveWebMVC.Models;
using diveWebMVC.ViewModels;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Http.HttpResults;

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
        public async Task<IActionResult> Index(string searchString,int? categoryId)
        {
            //var diveShopperContext = _context.TUproducts.Include(t => t.Category).Include(t => t.ProductCondition).Include(t => t.Seller); 
            var productsQuery = _context.TUproducts.AsQueryable();
            //if (!string.IsNullOrEmpty(searchString)) {
            //    productsQuery = productsQuery.Where(p=>p.ProductName)
            //}
            if (!string.IsNullOrEmpty(searchString))
            {
                productsQuery = productsQuery.Where(p => p.ProductName.Contains(searchString));
            }

            if (categoryId.HasValue)
            {
                productsQuery = productsQuery.Where(p => p.CategoryId == categoryId.Value);
            }

            var TUproductsviewmodels = await productsQuery.Select(p=>new TUproductsviewmodels {
                ProductId=p.ProductId,
                SellerId=p.SellerId,
                //SellerName=p.SellerName,
                CategoryId =p.CategoryId,
                ProductName = p.ProductName,
                ProductDescription = p.ProductDescription,
                ProductPrice = p.ProductPrice,
                UpdatedAt = p.UpdatedAt,
                CreatedAt = p.CreatedAt,
                ProductConditionId = p.ProductConditionId,
                ProductStatus = p.ProductStatus,
                //Image = null,
                TUproductImages = p.TUproductImages ?? new List<TUproductImage>()
            }).ToListAsync();
            
            
            //return View(await diveShopperContext.ToListAsync());
            return View(TUproductsviewmodels);
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
        public async Task<IActionResult> GetPicture(int id)
        {
            var product = await _context.TUproducts
                .Include(p => p.TUproductImages)
                .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product?.TUproductImages?.Any() == true)
            {
                var image = product.TUproductImages.FirstOrDefault(); // 假設您只想顯示第一張圖片
                if (image?.Image != null)
                {
                    return File(image.Image, "image/jpeg");
                }
            }

            // 如果沒有找到圖片或沒有圖片，返回一個默認圖片或404
            return NotFound();
        }

    }
}

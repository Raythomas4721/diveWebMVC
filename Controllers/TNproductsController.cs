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
        public async Task<IActionResult> Index(string searchString)
        {
            IQueryable<TNproduct> result = _context.TNproducts;
            if (!String.IsNullOrEmpty(searchString))
            {
                result = result.Where(s => s.ProductName.Contains(searchString));
            }
            var products = await result.Select(c => new TNproduct
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,

                UnitCost = c.UnitCost,
                Description = c.Description,
                Picture = null
            }).ToListAsync();
            return View(products);
        }
        public async Task<FileResult> GetPicture(int id)
        {

            TNproduct? c = await _context.TNproducts.FindAsync(id);
            byte[]? content = c?.Picture;
            return File(content, "image/jpeg");
        }
        // GET: TNproducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproduct = await _context.TNproducts.Select(c => new TNproduct
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,

                UnitCost = c.UnitCost,
                Description = c.Description,
                Picture = null
            }).FirstOrDefaultAsync(m => m.ProductId == id);
            if (tNproduct == null)
            {
                return NotFound();
            }

            return PartialView("_Details", tNproduct);
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
                if (Request.Form.Files["Picture"] != null)
                {
                    ReadUpLoadImage(tNproduct);
                }
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

            var tNproduct = await _context.TNproducts.Select(c => new TNproduct
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,

                UnitCost = c.UnitCost,
                Description = c.Description,
                Picture = null
            }).FirstOrDefaultAsync(m => m.ProductId == id);
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
                TNproduct? c = await _context.TNproducts.FindAsync(id);
                if (Request.Form.Files["Picture"] != null)
                {
                    ReadUpLoadImage(tNproduct);
                }
                else
                {
                    tNproduct.Picture = c.Picture;
                }
                _context.Entry(c).State = EntityState.Detached;



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
        private void ReadUpLoadImage(TNproduct tNproduct)
        {
            using (BinaryReader reader = new BinaryReader(Request.Form.Files["Picture"].OpenReadStream()))
            {
                tNproduct.Picture = reader.ReadBytes((int)Request.Form.Files["Picture"].Length);
            }
        }

        // GET: TNproducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproduct = await _context.TNproducts.Select(c => new TNproduct
            {
                ProductId = c.ProductId,
                ProductName = c.ProductName,

                UnitCost = c.UnitCost,
                Description = c.Description,
                Picture = null
            }).FirstOrDefaultAsync(m => m.ProductId == id);
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

        public async Task<IActionResult> searchName(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                return BadRequest("Search term is empty.");
            }

            // 查詢是否存在商品名稱完全匹配的商品
            var products = await _context.TNproducts
                .Where(e => e.ProductName.Contains(p))  // 使用 Contains 做模糊匹配
                .ToListAsync();

            // 如果找不到任何商品，返回 NotFound
            if (!products.Any())
            {
                return NotFound("No products found.");
            }

            // 返回部分視圖，並將商品資料傳遞給視圖
            return PartialView("_FindPartial", products);
        }



        //bool Exists = _context.TNproducts.Any(e => e.ProductName == p);
        //return Exists ? "true" : "false";

        private bool TNproductExists(int id)
        {
            return _context.TNproducts.Any(e => e.ProductId == id);
        }
    }
}

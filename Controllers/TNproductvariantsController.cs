using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diveWebMVC.Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace diveWebMVC.Controllers
{
    public class TNproductvariantsController : Controller
    {
        private readonly diveShopperContext _context;

        public TNproductvariantsController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TNproductvariants/Index
        public async Task<IActionResult> Index()
        {
            var diveShopperContext = _context.TNproductvariants.Include(t => t.Product).Include(t => t.Color).Include(t => t.Gender).Include(t => t.Size).Include(t => t.Thickness);
            return View(diveShopperContext);
        }
        // GET: TNproductvariants/IndexJson
        public JsonResult IndexJson()
        {
            try
            {
                var diveShopperContext = _context.TNproductvariants
                    .Include(t => t.Product)
                    .Include(t => t.Color)
                    .Include(t => t.Gender)
                    .Include(t => t.Size)
                    .Include(t => t.Thickness)
                    .Select(pv => new
                    {
                        pv.ProductvariantsId,
                        pv.ProductId,
                        pv.SizeId,
                        pv.ColorId,
                        pv.ThicknessId,
                        pv.GenderId,
                        pv.UnitPrice,
                        pv.Stock,
                        ProductName = pv.Product.ProductName,  // 假設有 Product.Name 欄位
                        SizeName = pv.Size.Size,        // 假設有 Size.Name 欄位
                        ColorName = pv.Color.Color,      // 假設有 Color.Name 欄位
                        ThicknessName = pv.Thickness.Thickness,  // 假設有 Thickness.Name 欄位
                        GenderName = pv.Gender.Gender     // 假設有 Gender.Name 欄位
                    })
                    .ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve
                };
                return Json(diveShopperContext, options);
            }
            catch (Exception ex)
            {
                // 可以在這裡記錄錯誤，或者處理異常
                return null;
            }
        }
        // GET: TNproductvariants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproductvariant = await _context.TNproductvariants
                .Include(t => t.Product)
                .Include(t => t.Color)
                .Include(t => t.Gender)
                .Include(t => t.Size)
                .Include(t => t.Thickness)
                .FirstOrDefaultAsync(m => m.ProductvariantsId == id);
            if (tNproductvariant == null)
            {
                return NotFound();
            }

            return View(tNproductvariant);
        }

        // GET: TNproductvariants/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.TNproducts, "ProductId", "ProductName");
            ViewData["ColorId"] = new SelectList(_context.TNcolors, "ColorId", "Color");
            ViewData["GenderId"] = new SelectList(_context.TNgenders, "GenderId", "Gender");
            ViewData["SizeId"] = new SelectList(_context.TNsizes, "SizeId", "Size");
            ViewData["ThicknessId"] = new SelectList(_context.TNthicknesses, "ThicknessId", "Thickness");
            return View();
        }

        // POST: TNproductvariants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductvariantsId,ProductId,SizeId,ColorId,ThicknessId,GenderId,UnitPrice,Stock")] TNproductvariant tNproductvariant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tNproductvariant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.TNproducts, "ProductId", "ProductId", tNproductvariant.ProductId);
            ViewData["ColorId"] = new SelectList(_context.TNcolors, "ColorId", "ColorId", tNproductvariant.ColorId);
            ViewData["GenderId"] = new SelectList(_context.TNgenders, "GenderId", "GenderId", tNproductvariant.GenderId);
            ViewData["SizeId"] = new SelectList(_context.TNsizes, "SizeId", "SizeId", tNproductvariant.SizeId);
            ViewData["ThicknessId"] = new SelectList(_context.TNthicknesses, "ThicknessId", "ThicknessId", tNproductvariant.ThicknessId);
            return View(tNproductvariant);
        }

//        [HttpGet]
//        public IActionResult GetProductDetails(int id)
//        {
//            var product = _context.TNproducts.FirstOrDefault(p => p.ProductId == id);

//            if (product == null)
//            {
//                return Json(new { success = false });
//            }

//            // 根据商品名称判断是否需要显示尺寸和性别
//            bool needsSize = product.ProductName.Contains("衣") || product.ProductName.Contains("BCD") || product.ProductName.Contains("");
//            bool needsGender = product.ProductName.Contains("衣");

//            // 返回商品是否需要显示某些下拉框的标志
//            var result = new
//            {
//                needsSize = needsSize,      // 是否需要尺寸
//                needsGender = needsGender   // 是否需要性别
//            };

//            return Json(result);  // 返回 JSON 格式的数据
//        }
//}



        // GET: TNproductvariants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproductvariant = await _context.TNproductvariants.FindAsync(id);
            if (tNproductvariant == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.TNproducts, "ProductId", "ProductName", tNproductvariant.ProductId);
            ViewData["ColorId"] = new SelectList(_context.TNcolors, "ColorId", "Color", tNproductvariant.ColorId);
            ViewData["GenderId"] = new SelectList(_context.TNgenders, "GenderId", "Gender", tNproductvariant.GenderId);
            ViewData["SizeId"] = new SelectList(_context.TNsizes, "SizeId", "Size", tNproductvariant.SizeId);
            ViewData["ThicknessId"] = new SelectList(_context.TNthicknesses, "ThicknessId", "Thickness", tNproductvariant.ThicknessId);
            return View(tNproductvariant);
        }

        // POST: TNproductvariants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductvariantsId,ProductId,SizeId,ColorId,ThicknessId,GenderId,UnitPrice,Stock")] TNproductvariant tNproductvariant)
        {
            if (id != tNproductvariant.ProductvariantsId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tNproductvariant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TNproductvariantExists(tNproductvariant.ProductvariantsId))
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
            ViewData["ProductId"] = new SelectList(_context.TNproducts, "ProductId", "ProductId", tNproductvariant.ProductId);
            ViewData["ColorId"] = new SelectList(_context.TNcolors, "ColorId", "ColorId", tNproductvariant.ColorId);
            ViewData["GenderId"] = new SelectList(_context.TNgenders, "GenderId", "GenderId", tNproductvariant.GenderId);
            ViewData["SizeId"] = new SelectList(_context.TNsizes, "SizeId", "SizeId", tNproductvariant.SizeId);
            ViewData["ThicknessId"] = new SelectList(_context.TNthicknesses, "ThicknessId", "ThicknessId", tNproductvariant.ThicknessId);
            return View(tNproductvariant);
        }

        // GET: TNproductvariants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tNproductvariant = await _context.TNproductvariants
                .Include(t => t.Product)
                .Include(t => t.Color)
                .Include(t => t.Gender)
                .Include(t => t.Size)
                .Include(t => t.Thickness)
                .FirstOrDefaultAsync(m => m.ProductvariantsId == id);
            if (tNproductvariant == null)
            {
                return NotFound();
            }

            return View(tNproductvariant);
        }

        // POST: TNproductvariants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tNproductvariant = await _context.TNproductvariants.FindAsync(id);
            if (tNproductvariant != null)
            {
                _context.TNproductvariants.Remove(tNproductvariant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TNproductvariantExists(int id)
        {
            return _context.TNproductvariants.Any(e => e.ProductvariantsId == id);
        }
    }
}

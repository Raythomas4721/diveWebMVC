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
            // 如果沒有在Session中找到登入資訊，跳轉回登入頁面
            if (HttpContext.Session.GetString("AdminId") == null)
            {
                return RedirectToAction("Login", "TMadmins");
            }

            IQueryable<TNproduct> query = _context.TNproducts.Include(p => p.TNpictures);

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(p => p.ProductName.Contains(searchString));
            }

            var products = await query.ToListAsync();
            return View(products);
        }


        public async Task<FileResult> GetPicture(int id)
        {

            TNproduct? c = await _context.TNproducts.FindAsync(id);
            if (c?.Picture == null)
            {
                // 回傳預設圖
                var noImagePath = Path.Combine("wwwroot/images/noimage.png");
                var noImageData = System.IO.File.ReadAllBytes(noImagePath);
                return File(noImageData, "image/png");
            }
            return File(c.Picture, "image/jpeg");
        }
        // GET: TNproducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            // 一次載入 TNpictures (多張圖片)
            var product = await _context.TNproducts
                                        .Include(p => p.TNpictures)
                                        .FirstOrDefaultAsync(p => p.ProductId == id);

            if (product == null) return NotFound();

            return PartialView("_Details", product);
        }
        [HttpGet]
        public async Task<IActionResult> GetPictureByPicId(int picId)
        {
            var picture = await _context.TNpictures.FindAsync(picId);
            if (picture?.Image == null || picture.Image.Length == 0)
            {
                // 回傳預設圖
                var noImagePath = Path.Combine("wwwroot", "images", "noimage.png");
                var noImageData = System.IO.File.ReadAllBytes(noImagePath);
                return File(noImageData, "image/png");
            }

            return File(picture.Image, "image/jpeg");
        }


        // GET: TNproducts/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TNproduct tNproduct, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                // 1) 先新增 TNproduct (但此時還沒處理多張照片)
                _context.TNproducts.Add(tNproduct);
                await _context.SaveChangesAsync(); // 這裡執行後，就能拿到 tNproduct.ProductId

                // 2) 把多張檔案存到 TNpictures
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.CopyToAsync(ms);
                                var data = ms.ToArray();

                                // 建立一筆 TNpicture，設定好 ProductId
                                var pic = new TNpicture
                                {
                                    ProductId = tNproduct.ProductId,
                                    Image = data
                                };
                                _context.TNpictures.Add(pic);
                            }
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }
            return View(tNproduct);
        }

        // GET: TNproducts/Edit/5
        // GET: TNproducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var tNproduct = await _context.TNproducts
                                         .Include(p => p.TNpictures)
                                          .FirstOrDefaultAsync(m => m.ProductId == id);

            if (tNproduct == null) return NotFound();

            return View(tNproduct);
        }


        // POST: TNproducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ProductId,ProductName,UnitCost,Description")] TNproduct tNproduct,
            List<IFormFile> files)
        {
            if (id != tNproduct.ProductId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // 取得資料庫中現有的商品記錄
                var existingProduct = await _context.TNproducts.FindAsync(id);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                // 更新商品的基本資訊
                existingProduct.ProductName = tNproduct.ProductName;
                existingProduct.UnitCost = tNproduct.UnitCost;
                existingProduct.Description = tNproduct.Description;

                // 對商品進行更新標記
                _context.Entry(existingProduct).State = EntityState.Modified;

                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        if (file.Length > 0)
                        {
                            using (var ms = new MemoryStream())
                            {
                                await file.CopyToAsync(ms);
                                byte[] imageData = ms.ToArray();

                                var picture = new TNpicture
                                {
                                    ProductId = existingProduct.ProductId,
                                    Image = imageData
                                };
                                _context.TNpictures.Add(picture);
                            }
                        }
                    }
                }

                try
                {
                    // 儲存所有變更（包括商品更新和圖片新增）
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

            // 若模型驗證失敗，返回原視圖以顯示錯誤訊息
            return View(tNproduct);
        }

        // 輔助方法：確認商品是否存在
        private bool TNproductExists(int id)
        {
            return _context.TNproducts.Any(e => e.ProductId == id);
        }

        private void ReadUpLoadImage(TNpicture image)
        {
            using (BinaryReader reader = new BinaryReader(Request.Form.Files["Image"].OpenReadStream()))
            {
                image.Image = reader.ReadBytes((int)Request.Form.Files["Image"].Length);
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
            }).Include(p => p.TNpictures).FirstOrDefaultAsync(m => m.ProductId == id);
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

        

        
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diveWebMVC.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace diveWebMVC.Controllers
{
    public class TCcoursesController : Controller
    {
        private readonly diveShopperContext _context;

        public TCcoursesController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TCcourses
        public async Task<IActionResult> Index()
        {
            //// 使用 Include 來確保 CourseCategory, Level, Coach 被一起載入(by AI)
            //var diveShopperContext = _context.TCcourses
            //    .Include(t => t.CourseCategory)  // 包含 CourseCategory
            //    .Include(t => t.Level)           // 包含 Level
            //    .Include(t => t.Coach);          // 包含 Coach

            //// 在這裡處理 Photo 欄位設定為 null，其他資料會正常載入
            //var courses = await diveShopperContext
            //    .Select(c => new TCcourse
            //    {
            //        CourseId = c.CourseId,
            //        CourseCategoryId = c.CourseCategoryId,
            //        LevelId = c.LevelId,
            //        CoachId = c.CoachId,
            //        CoursePrice = c.CoursePrice,
            //        Photo = null,  // 設定 Photo 為 null
            //        CreatedAt = c.CreatedAt,
            //        UpdatedAt = c.UpdatedAt
            //    }).ToListAsync();
            //return View(courses);

            //var diveShopperContext = _context.TCcourses.Include(t => t.CourseCategory).Include(t => t.Level).Include(t => t.Coach);
            //return View(diveShopperContext.Select(c => new TCcourse
            //{
            //    CourseId = c.CourseId,
            //    CourseCategoryId = c.CourseCategoryId,
            //    LevelId = c.LevelId,
            //    CoachId = c.CoachId,
            //    CoursePrice = c.CoursePrice,
            //    Photo = null,
            //    CreatedAt = c.CreatedAt,
            //    UpdatedAt = c.UpdatedAt
            //}));

            var diveShopperContext = _context.TCcourses.Include(t => t.CourseCategory).Include(t => t.Level).Include(t => t.Coach);
            return View(diveShopperContext);
            //return View(await diveShopperContext.ToListAsync());
        }

        //GET: TCcourses/GetPicture/id
        public async Task<FileResult> GetPicture(int id)
        {
            TCcourse? c = await _context.TCcourses.FindAsync(id);
            byte[]? content = c?.Photo;
            return File(content, "image/jpeg");
        }
        
        // GET: TCcourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var tCcourse = await _context.TCcourses
            //    .Include(t => t.Coach)
            //    .Include(t => t.CourseCategory)
            //    .Include(t => t.Level)
            //    .Select(c => new TCcourse
            //    { //FirstOrDefaultAsync沒有Select -> 改FindAsync(傳主索引鍵)
            //        CourseId = c.CourseId,
            //        CourseCategoryId = c.CourseCategoryId,
            //        LevelId = c.LevelId,
            //        CoachId = c.CoachId,
            //        CoursePrice = c.CoursePrice,
            //        Photo = null,
            //        CreatedAt = c.CreatedAt,
            //        UpdatedAt = c.UpdatedAt
            //    }).FirstOrDefaultAsync(c => c.CourseId == id); 
            var tCcourse = await _context.TCcourses
                .Include(t => t.Coach)
                .Include(t => t.CourseCategory)
                .Include(t => t.Level)
                .FirstOrDefaultAsync(m => m.CourseId == id);
            if (tCcourse == null)
            {
                return NotFound();
            }

            return View(tCcourse);
        }

        // GET: TCcourses/Create
        public IActionResult Create()
        {
            ViewData["CoachId"] = new SelectList(_context.TMcoaches, "CoachId", "CoachId");
            ViewData["CoachName"] = new SelectList(_context.TMcoaches, "CoachName", "CoachName");
            ViewData["CourseCategoryId"] = new SelectList(_context.TCcourseCategories, "CourseCategoryId", "CourseCategoryId");
            ViewData["CategoryName"] = new SelectList(_context.TCcourseCategories, "CategoryName", "CategoryName");
            ViewData["LevelId"] = new SelectList(_context.TCcourseLevels, "LevelId", "LevelId");
            ViewData["LevelName"] = new SelectList(_context.TCcourseLevels, "LevelName", "LevelName");
            return View();
        }

        // POST: TCcourses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseId,CourseCategoryId,LevelId,CoachId,CoursePrice,Photo,CreatedAt,UpdatedAt")] TCcourse tCcourse)
        {
            if (ModelState.IsValid)
            {
                if (Request.Form.Files["Photo"] != null)
                {

                    using (BinaryReader reader = new BinaryReader(Request.Form.Files["Photo"].OpenReadStream()))
                    {
                        tCcourse.Photo = reader.ReadBytes((int)Request.Form.Files["Photo"].Length);
                    }
                }
                _context.Add(tCcourse);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CoachId"] = new SelectList(_context.TMcoaches, "CoachId", "CoachId", tCcourse.CoachId);
            ViewData["CourseCategoryId"] = new SelectList(_context.TCcourseCategories, "CourseCategoryId", "CourseCategoryId", tCcourse.CourseCategoryId);
            ViewData["LevelId"] = new SelectList(_context.TCcourseLevels, "LevelId", "LevelId", tCcourse.LevelId);
            return View(tCcourse);
        }

        // GET: TCcourses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           
            var tCcourse = await _context.TCcourses.Select(c => new TCcourse 
            {
                CourseId = c.CourseId,
                CourseCategoryId = c.CourseCategoryId,
                LevelId = c.LevelId,
                CoachId = c.CoachId,
                CoursePrice = c.CoursePrice,
                Photo = null,
                CreatedAt = c.CreatedAt,
                UpdatedAt = c.UpdatedAt
            }).FirstOrDefaultAsync(m => m.CourseId == id);
            //var tCcourse = await _context.TCcourses.FindAsync(id);
            
            if (tCcourse == null)
            {
                return NotFound();
            }
            ViewData["CoachId"] = new SelectList(_context.TMcoaches, "CoachId", "CoachId", tCcourse.CoachId);
            ViewData["CourseCategoryId"] = new SelectList(_context.TCcourseCategories, "CourseCategoryId", "CourseCategoryId", tCcourse.CourseCategoryId);
            ViewData["LevelId"] = new SelectList(_context.TCcourseLevels, "LevelId", "LevelId", tCcourse.LevelId);
            return View(tCcourse);
        }

        // POST: TCcourses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseId,CourseCategoryId,LevelId,CoachId,CoursePrice,Photo,CreatedAt,UpdatedAt")] TCcourse tCcourse)
        {
            if (id != tCcourse.CourseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //先把要編輯的紀錄找出來(取得原圖)
                TCcourse? c = await _context.TCcourses.FindAsync(id);
                //                asp-for生成的name
                if (Request.Form.Files["Photo"] != null) {

                    using (BinaryReader reader = new BinaryReader(Request.Form.Files["Photo"].OpenReadStream())) 
                    {
                        tCcourse.Photo = reader.ReadBytes((int)Request.Form.Files["Photo"].Length);
                    }
                }
                else
                {
                    tCcourse.Photo = c.Photo; //使用者未上傳圖案，維持原圖
                }
                _context.Entry(c).State = EntityState.Detached; //卸離C，只追蹤category
                try
                {
                    _context.Update(tCcourse);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCcourseExists(tCcourse.CourseId))
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
            ViewData["CoachId"] = new SelectList(_context.TMcoaches, "CoachId", "CoachId", tCcourse.CoachId);
            ViewData["CourseCategoryId"] = new SelectList(_context.TCcourseCategories, "CourseCategoryId", "CourseCategoryId", tCcourse.CourseCategoryId);
            ViewData["LevelId"] = new SelectList(_context.TCcourseLevels, "LevelId", "LevelId", tCcourse.LevelId);
            return View(tCcourse);
        }

        // GET: TCcourses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            //var tCcourse = await _context.TCcourses.Select(c => new TCcourse
            //{
            //    CourseId = c.CourseId,
            //    CourseCategoryId = c.CourseCategoryId,
            //    LevelId = c.LevelId,
            //    CoachId = c.CoachId,
            //    CoursePrice = c.CoursePrice,
            //    Photo = null,
            //    CreatedAt = c.CreatedAt,
            //    UpdatedAt = c.UpdatedAt
            //}).FirstOrDefaultAsync(m => m.CourseId == id);
            var tCcourse = await _context.TCcourses
                .Include(t => t.Coach)
                .Include(t => t.CourseCategory)
                .Include(t => t.Level)
                .FirstOrDefaultAsync(m => m.CourseId == id);

            if (tCcourse == null)
            {
                return NotFound();
            }         
            return View(tCcourse);
        }

        // POST: TCcourses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCcourse = await _context.TCcourses.FindAsync(id);
            if (tCcourse != null)
            {
                _context.TCcourses.Remove(tCcourse);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCcourseExists(int id)
        {
            return _context.TCcourses.Any(e => e.CourseId == id);
        }
    }
}

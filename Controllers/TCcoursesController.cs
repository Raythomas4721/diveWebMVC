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
    public class TCcoursesController : Controller
    {
        private readonly diveShopperContext _context;

        public TCcoursesController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TCcourses
        public IActionResult Index()
        {
            return View();
        }
        //從這裡繼續
        

        // GET: TCcourses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

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
            ViewData["CourseCategoryId"] = new SelectList(_context.TCcourseCategories, "CourseCategoryId", "CourseCategoryId");
            ViewData["LevelId"] = new SelectList(_context.TCcourseLevels, "LevelId", "LevelId");
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

            var tCcourse = await _context.TCcourses.FindAsync(id);
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

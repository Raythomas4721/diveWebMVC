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
    public class TCcourseCategoriesController : Controller
    {
        private readonly diveShopperContext _context;

        public TCcourseCategoriesController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TCcourseCategories
        public async Task<IActionResult> Index()
        {
            return View(await _context.TCcourseCategories.ToListAsync());
        }

        // GET: TCcourseCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCcourseCategory = await _context.TCcourseCategories
                .FirstOrDefaultAsync(m => m.CourseCategoryId == id);
            if (tCcourseCategory == null)
            {
                return NotFound();
            }

            return View(tCcourseCategory);
        }

        // GET: TCcourseCategories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TCcourseCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CourseCategoryId,CategoryName,Description,Duration,Quota")] TCcourseCategory tCcourseCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCcourseCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tCcourseCategory);
        }

        // GET: TCcourseCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCcourseCategory = await _context.TCcourseCategories.FindAsync(id);
            if (tCcourseCategory == null)
            {
                return NotFound();
            }
            return View(tCcourseCategory);
        }

        // POST: TCcourseCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CourseCategoryId,CategoryName,Description,Duration,Quota")] TCcourseCategory tCcourseCategory)
        {
            if (id != tCcourseCategory.CourseCategoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCcourseCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCcourseCategoryExists(tCcourseCategory.CourseCategoryId))
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
            return View(tCcourseCategory);
        }

        // GET: TCcourseCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tCcourseCategory = await _context.TCcourseCategories
                .FirstOrDefaultAsync(m => m.CourseCategoryId == id);
            if (tCcourseCategory == null)
            {
                return NotFound();
            }

            return View(tCcourseCategory);
        }

        // POST: TCcourseCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tCcourseCategory = await _context.TCcourseCategories.FindAsync(id);
            if (tCcourseCategory != null)
            {
                _context.TCcourseCategories.Remove(tCcourseCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCcourseCategoryExists(int id)
        {
            return _context.TCcourseCategories.Any(e => e.CourseCategoryId == id);
        }
    }
}

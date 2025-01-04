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
    public class TNproductvariantsController : Controller
    {
        private readonly diveShopperContext _context;

        public TNproductvariantsController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TNproductvariants
        public async Task<IActionResult> Index()
        {
            var diveShopperContext = _context.TNproductvariants.Include(t => t.Product).Include(t => t.Color).Include(t => t.Gender).Include(t => t.Size).Include(t => t.Thickness);
            return View(await diveShopperContext.ToListAsync());
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
            ViewData["ProductId"] = new SelectList(_context.TNproducts, "ProductId", "ProductId", tNproductvariant.ProductId);
            ViewData["ColorId"] = new SelectList(_context.TNcolors, "ColorId", "ColorId", tNproductvariant.ColorId);
            ViewData["GenderId"] = new SelectList(_context.TNgenders, "GenderId", "GenderId", tNproductvariant.GenderId);
            ViewData["SizeId"] = new SelectList(_context.TNsizes, "SizeId", "SizeId", tNproductvariant.SizeId);
            ViewData["ThicknessId"] = new SelectList(_context.TNthicknesses, "ThicknessId", "ThicknessId", tNproductvariant.ThicknessId);
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

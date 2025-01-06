using diveWebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diveWebMVC.Controllers
{
    public class SiteController : Controller
    {

        private readonly diveShopperContext _context;

        public SiteController(diveShopperContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.TSsiteDetails.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SiteId,VenueName,NumberOfPeople,VenueAddress,Detail,State")] TSsiteDetail TSsiteDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(TSsiteDetail);
                await _context.SaveChangesAsync();//儲存
                return RedirectToAction(nameof(Index));
            }
            return View(TSsiteDetail);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TSsiteDetail = await _context.TSsiteDetails
                .FirstOrDefaultAsync(m => m.SiteId == id);
            if (TSsiteDetail == null)
            {
                return NotFound();
            }

            return View(TSsiteDetail);
        }

        // POST: TMmemberLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var TSsiteDetail = await _context.TSsiteDetails.FindAsync(id);
            if (TSsiteDetail != null)
            {
                _context.TSsiteDetails.Remove(TSsiteDetail);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TSsiteDetail = await _context.TSsiteDetails
                .FirstOrDefaultAsync(m => m.SiteId == id);
            if (TSsiteDetail == null)
            {
                return NotFound();
            }

            return View(TSsiteDetail);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var TSsiteDetail = await _context.TSsiteDetails.FindAsync(id);
            if (TSsiteDetail == null)
            {
                return NotFound();
            }
            return View(TSsiteDetail);
        }

        // POST: TMmemberLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SiteId,VenueName,NumberOfPeople,VenueAddress,Detail,State")] TSsiteDetail TSsiteDetail)
        {
            if (id != TSsiteDetail.SiteId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(TSsiteDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TSsiteDetailExists(TSsiteDetail.SiteId))
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
            return View(TSsiteDetail);
        }
        private bool TSsiteDetailExists(int id)
        {
            return _context.TMmemberLists.Any(e => e.MemberId == id);
        }
    }
}

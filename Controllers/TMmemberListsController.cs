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
    public class TMmemberListsController : Controller
    {
        private readonly diveShopperContext _context;

        public TMmemberListsController(diveShopperContext context)
        {
            _context = context;
        }

        // GET: TMmemberLists
        public async Task<IActionResult> Index()
        {
            return View(await _context.TMmemberLists.ToListAsync());
        }

        // GET: TMmemberLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMmemberList = await _context.TMmemberLists
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (tMmemberList == null)
            {
                return NotFound();
            }

            return View(tMmemberList);
        }

        // GET: TMmemberLists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TMmemberLists/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MemberId,MemberName,MemberGender,MemberPhone,MemberEmail,MemberAddress,MemberPassword,UrgentContact,UrgentPhone,MemberPhoto,RecentLogin")] TMmemberList tMmemberList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tMmemberList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tMmemberList);
        }

        // GET: TMmemberLists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMmemberList = await _context.TMmemberLists.FindAsync(id);
            if (tMmemberList == null)
            {
                return NotFound();
            }
            return View(tMmemberList);
        }

        // POST: TMmemberLists/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,MemberName,MemberGender,MemberPhone,MemberEmail,MemberAddress,MemberPassword,UrgentContact,UrgentPhone,MemberPhoto,RecentLogin")] TMmemberList tMmemberList)
        {
            if (id != tMmemberList.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tMmemberList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TMmemberListExists(tMmemberList.MemberId))
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
            return View(tMmemberList);
        }

        // GET: TMmemberLists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMmemberList = await _context.TMmemberLists
                .FirstOrDefaultAsync(m => m.MemberId == id);
            if (tMmemberList == null)
            {
                return NotFound();
            }

            return View(tMmemberList);
        }

        // POST: TMmemberLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tMmemberList = await _context.TMmemberLists.FindAsync(id);
            if (tMmemberList != null)
            {
                _context.TMmemberLists.Remove(tMmemberList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TMmemberListExists(int id)
        {
            return _context.TMmemberLists.Any(e => e.MemberId == id);
        }
    }
}

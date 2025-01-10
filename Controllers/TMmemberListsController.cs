using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using diveWebMVC.Models;
using System.Drawing;

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
        public async Task<IActionResult> Index(string searchText)
        {
            // 查詢基礎資料
            IQueryable<TMmemberList> query = _context.TMmemberLists;

            // 若有搜尋文字，加入篩選條件
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(p => p.MemberName.Contains(searchText));
            }

            // 查詢並選取需要的欄位
            var memberList = await query.Select(c => new TMmemberList
            {
                MemberId = c.MemberId,
                MemberName = c.MemberName,
                MemberGender = c.MemberGender,
                MemberPhone = c.MemberPhone,
                MemberEmail = c.MemberEmail,
                MemberAddress = c.MemberAddress,
                MemberPassword = c.MemberPassword,
                MemberPhoto = null,
                UrgentContact = c.UrgentContact,
                UrgentPhone = c.UrgentPhone,
                RecentLogin = c.RecentLogin
            }).ToListAsync();

            // 回傳資料至 View，並保留搜尋文字以供回填
            ViewData["SearchText"] = searchText;
            return View(memberList);
        }

        // 加入GetPicture方法
        public async Task<FileResult> GetPicture(int id)
        {
            TMmemberList? member = await _context.TMmemberLists.FindAsync(id);
            byte[]? content = member?.MemberPhoto;
            return File(content, "image/jpeg");

        }

        // GET: TMmemberLists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tMmemberList = await _context.TMmemberLists.Select(c => new TMmemberList
            {
                MemberId = c.MemberId,
                MemberName = c.MemberName,
                MemberGender = c.MemberGender,
                MemberPhone = c.MemberPhone,
                MemberEmail = c.MemberEmail,
                MemberAddress = c.MemberAddress,
                MemberPassword = c.MemberPassword,
                MemberPhoto = null,
                UrgentContact = c.UrgentContact,
                UrgentPhone = c.UrgentPhone,
                RecentLogin = c.RecentLogin

            }).FirstOrDefaultAsync(m => m.MemberId == id);
            if (tMmemberList == null)
            {
                return NotFound();
            }

            return View(tMmemberList);
        }

        // GET: TMmemberLists/Create
        public IActionResult Create()
        {
            return PartialView();
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
                if (Request.Form.Files["MemberPhoto"] != null)
                {
                    using (BinaryReader reader = new BinaryReader(Request.Form.Files["MemberPhoto"].OpenReadStream()))
                    {
                        tMmemberList.MemberPhoto = reader.ReadBytes((int)Request.Form.Files["MemberPhoto"].Length);
                    }
                }
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

            var tMmemberList = await _context.TMmemberLists.Select(c => new TMmemberList
            {
                MemberId = c.MemberId,
                MemberName = c.MemberName,
                MemberGender = c.MemberGender,
                MemberPhone = c.MemberPhone,
                MemberEmail = c.MemberEmail,
                MemberAddress = c.MemberAddress,
                MemberPassword = c.MemberPassword,
                MemberPhoto = null,
                UrgentContact = c.UrgentContact,
                UrgentPhone = c.UrgentPhone,
                RecentLogin = c.RecentLogin
            }).FirstOrDefaultAsync(m => m.MemberId == id);
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
        [RequestFormLimits(MultipartBodyLengthLimit =1048000)]
        [RequestSizeLimit(1048000)]
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
                    TMmemberList? m = await _context.TMmemberLists.FindAsync(tMmemberList.MemberId);
                    if (Request.Form.Files["MemberPhoto"] != null)
                    {
                        using (BinaryReader reader = new BinaryReader(Request.Form.Files["MemberPhoto"].OpenReadStream()))
                        {
                            tMmemberList.MemberPhoto = reader.ReadBytes((int)Request.Form.Files["MemberPhoto"].Length);
                        }
                    }
                    else
                    {
                        tMmemberList.MemberPhoto = m.MemberPhoto;
                    }
                    _context.Entry(m).State = EntityState.Detached;

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

            var tMmemberList = await _context.TMmemberLists.Select(c => new TMmemberList
            {
                MemberId = c.MemberId,
                MemberName = c.MemberName,
                MemberGender = c.MemberGender,
                MemberPhone = c.MemberPhone,
                MemberEmail = c.MemberEmail,
                MemberAddress = c.MemberAddress,
                MemberPassword = c.MemberPassword,
                MemberPhoto = null,
                UrgentContact = c.UrgentContact,
                UrgentPhone = c.UrgentPhone,
                RecentLogin = c.RecentLogin
            }).FirstOrDefaultAsync(m => m.MemberId == id);
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

using System.Diagnostics;
using diveWebMVC.Data;
using diveWebMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace diveWebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly diveShopperContext _context;

        public HomeController(ILogger<HomeController> logger, diveShopperContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString("AdminId")))
            {
                return RedirectToAction("Login", "TMadmins");
            }
            // 從 Session 取得 AdminId
            var adminId = HttpContext.Session.GetString("AdminId");

            if (!string.IsNullOrEmpty(adminId))
            {
                // 根據 AdminId 取得使用者資訊
                var admin = _context.TMadmins.FirstOrDefault(a => a.AdminId.ToString() == adminId);

                if (admin != null)
                {
                    ViewData["AdminName"] = admin.UserName; // 將名稱傳到 View
                    ViewData["AdminEmail"] = admin.Email;  // 可選，傳遞其他資訊
                }
            }
            else
            {
                // Session 無效，導回登入頁
                return RedirectToAction("Login");
            }

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

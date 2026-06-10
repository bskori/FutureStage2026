using FutureStage2026.Data;
using FutureStage2026.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var admin = _context.Admins.FirstOrDefault(x => x.Email == model.EmailId && x.Password == model.Password);

            if(admin == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(model);
            }

            HttpContext.Session.SetString("AdminId", admin.Id.ToString());

            return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction(nameof(Login));
        }
    }
}

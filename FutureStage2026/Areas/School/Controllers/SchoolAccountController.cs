using FutureStage2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class SchoolAccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolAccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var school = _context.Schools
                .FirstOrDefault(s => s.EmailId == email);

            if (school != null)
            {
                HttpContext.Session.SetString("SchoolId", school.Id.ToString());

                return RedirectToAction("Index", "Dashboard", new { area = "School" });
            }

            ViewBag.Error = "Invalid login";
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

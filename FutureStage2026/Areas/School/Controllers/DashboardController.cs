using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("SchoolId") == null)
            {
                return RedirectToAction("Login", "School", new { area = ""});
            }
            return View();
        }
    }
}

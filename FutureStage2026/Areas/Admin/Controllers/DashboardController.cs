using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            if(HttpContext.Session.GetString("AdminId") == null)
            {
                return RedirectToAction("Login", "Admin");
            }
            return View();
        }
    }
}

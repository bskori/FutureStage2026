using FutureStage2026.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

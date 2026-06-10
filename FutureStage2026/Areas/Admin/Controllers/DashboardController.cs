using FutureStage2026.Data;
using FutureStage2026.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AdminAuthorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // 📊 Dashboard Stats
            ViewBag.TotalSchools = _context.Schools.Count();
            ViewBag.TotalParents = _context.Parents.Count();
            ViewBag.TotalEnquiries = _context.Enquiries.Count();
            ViewBag.TotalReviews = _context.Reviews.Count();

            // 🆕 Latest Data (optional but powerful)
            ViewBag.RecentSchools = _context.Schools
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToList();

            ViewBag.RecentEnquiries = _context.Enquiries
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToList();

            return View();
        }
    }
}

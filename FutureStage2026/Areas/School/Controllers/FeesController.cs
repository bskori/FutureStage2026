using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class FeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FeesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult AddFees()
        {
            var schoolId = Convert.ToInt64(HttpContext.Session.GetString("SchoolId"));

            ViewBag.SchoolStandards = _context.SchoolStandards
                .Where(x => x.SchoolId == schoolId)
                .Include(x => x.Standard)
                .ToList();

            ViewBag.FeeHeads = _context.FeeHeads.ToList();

            ViewBag.SchoolId = schoolId;

            return View();
        }

        [HttpPost]
        public IActionResult AddFees(long schoolStandardId, long feeHeadId, decimal amount)
        {
            var data = new StandardFees
            {
                SchoolStandardId = schoolStandardId,
                FeeHeadId = feeHeadId,
                Amount = amount
            };

            _context.StandardFees.Add(data);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
    }
}

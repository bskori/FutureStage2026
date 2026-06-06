using FutureStage2026.Data;
using FutureStage2026.Models;
using FutureStage2026.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    //[SchoolAuthorize]
    public class DashboardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var schoolId = HttpContext.Session.GetString("SchoolId");

            var school = _context.Schools.Include(s => s.Area).FirstOrDefault(s => s.Id == Convert.ToInt64(schoolId));


            return View(school);
        }

        [HttpGet]
        public IActionResult Edit()
        {
            var schoolId = HttpContext.Session.GetString("SchoolId");

            var school = _context.Schools.FirstOrDefault(s => s.Id == Convert.ToInt64(schoolId));

            return View(school);
        }

        [HttpPost]
        public IActionResult Edit(FutureStage2026.Models.School model)
        {
            ModelState.Remove("PasswordHash");

            var existingSchool = _context.Schools.AsNoTracking().FirstOrDefault(s => s.Id == model.Id);

            if(existingSchool == null)
            {
                return NotFound();
            }

            model.PasswordHash = existingSchool.PasswordHash;
            model.AreaId = existingSchool.AreaId;   

            if (!ModelState.IsValid)            
                return View(model);

            _context.Schools.Update(model);
            _context.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}

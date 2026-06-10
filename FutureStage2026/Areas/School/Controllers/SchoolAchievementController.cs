using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class SchoolAchievementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolAchievementController(ApplicationDbContext context)
        {
            _context = context;
        }

        // ==================================================
        // GET LOGGED-IN SCHOOL ID
        // ==================================================

        private long GetLoggedInSchoolId()
        {
            return Convert.ToInt64(HttpContext.Session.GetString("SchoolId"));

        }

        // ==================================================
        // INDEX
        // ==================================================

        public IActionResult Index()
        {
            long schoolId = GetLoggedInSchoolId();

            var achievements = _context.SchoolAchievements
                .Where(x => x.SchoolId == schoolId)
                .OrderByDescending(x => x.SchoolAchievementDate)
                .ToList();

            return View(achievements);
        }

        // ==================================================
        // CREATE GET
        // ==================================================

        public IActionResult Create()
        {
            return View();
        }

        // ==================================================
        // CREATE POST
        // ==================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SchoolAchievement model)
        {
            if (ModelState.IsValid)
            {
                model.SchoolId = GetLoggedInSchoolId();

                _context.SchoolAchievements.Add(model);

                _context.SaveChanges();

                TempData["success"] =
                    "Achievement added successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // ==================================================
        // EDIT GET
        // ==================================================

        public IActionResult Edit(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var achievement = _context.SchoolAchievements
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // ==================================================
        // EDIT POST
        // ==================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SchoolAchievement model)
        {
            long schoolId = GetLoggedInSchoolId();

            var achievement = _context.SchoolAchievements
                .FirstOrDefault(x =>
                    x.Id == model.Id &&
                    x.SchoolId == schoolId);

            if (achievement == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                achievement.Title = model.Title;
                achievement.Desc = model.Desc;
                achievement.SchoolAchievementDate =
                    model.SchoolAchievementDate;

                _context.SaveChanges();

                TempData["success"] =
                    "Achievement updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        // ==================================================
        // DELETE GET
        // ==================================================
        [HttpGet]
        public IActionResult Delete(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var achievement = _context.SchoolAchievements
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (achievement == null)
            {
                return NotFound();
            }

            return View(achievement);
        }

        // ==================================================
        // DELETE POST
        // ==================================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var achievement = _context.SchoolAchievements
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (achievement == null)
            {
                return NotFound();
            }

            _context.SchoolAchievements.Remove(achievement);

            _context.SaveChanges();

            TempData["success"] =
                "Achievement deleted successfully.";

            return RedirectToAction(nameof(Index));
        }
    }
}

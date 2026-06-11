using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class SchoolStandardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolStandardController(ApplicationDbContext context)
        {
            _context = context;
        }

        private long GetLoggedInSchoolId()
        {
            return Convert.ToInt64(HttpContext.Session.GetString("SchoolId"));
        }

        // ==========================================
        // INDEX
        // ==========================================

        public IActionResult Index()
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolStandards
                .Include(x => x.Standard)
                .Where(x => x.SchoolId == schoolId)
                .OrderBy(x => x.Standard.StandardTitle)
                .ToList();

            return View(data);
        }

        // ==========================================
        // CREATE GET
        // ==========================================

        public IActionResult Create()
        {
            LoadStandards();

            return View();
        }

        // ==========================================
        // CREATE POST
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SchoolStandard model)
        {
            if (!ModelState.IsValid)
            {
                foreach(var error in ModelState.Values.SelectMany(x=>x.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }

            long schoolId = GetLoggedInSchoolId();

            bool alreadyExists = _context.SchoolStandards
                .Any(x =>
                    x.SchoolId == schoolId &&
                    x.StandardId == model.StandardId);

            if (alreadyExists)
            {
                ModelState.AddModelError("",
                    "This standard already exists.");
            }

            if (ModelState.IsValid)
            {
                model.SchoolId = schoolId;

                _context.SchoolStandards.Add(model);

                _context.SaveChanges();

                TempData["success"] =
                    "Standard added successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadStandards();

            return View(model);
        }

        // ==========================================
        // EDIT GET
        // ==========================================

        public IActionResult Edit(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolStandards
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            LoadStandards();

            return View(data);
        }

        // ==========================================
        // EDIT POST
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SchoolStandard model)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolStandards
                .FirstOrDefault(x =>
                    x.Id == model.Id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            bool duplicate = _context.SchoolStandards
                .Any(x =>
                    x.Id != model.Id &&
                    x.SchoolId == schoolId &&
                    x.StandardId == model.StandardId);

            if (duplicate)
            {
                ModelState.AddModelError("",
                    "This standard already exists.");
            }

            if (ModelState.IsValid)
            {
                data.StandardId = model.StandardId;
                data.IntakeCapacity = model.IntakeCapacity;

                _context.SaveChanges();

                TempData["success"] =
                    "Standard updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadStandards();

            return View(model);
        }

        // ==========================================
        // DELETE GET
        // ==========================================

        public IActionResult Delete(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolStandards
                .Include(x => x.Standard)
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            return View(data);
        }

        // ==========================================
        // DELETE POST
        // ==========================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolStandards
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            _context.SchoolStandards.Remove(data);

            _context.SaveChanges();

            TempData["success"] =
                "Standard deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================================
        // LOAD STANDARDS
        // ==========================================

        private void LoadStandards()
        {
            ViewBag.Standards = new SelectList(
                _context.Standards
                    .OrderBy(x => x.StandardTitle)
                    .ToList(),
                "Id",
                "StandardTitle"
            );
        }
    }
}

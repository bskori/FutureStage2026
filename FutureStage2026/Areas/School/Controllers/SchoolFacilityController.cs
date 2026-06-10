using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class SchoolFacilityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SchoolFacilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        private long GetLoggedInSchoolId()
        {
            return Convert.ToInt64(HttpContext.Session.GetString("SchoolId"));
        }

        // ==========================
        // INDEX
        // ==========================

        public IActionResult Index()
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolFacilities
                .Include(x => x.Facility)
                .Where(x => x.SchoolId == schoolId)
                .ToList();

            return View(data);
        }

        // ==========================
        // CREATE GET
        // ==========================

        public IActionResult Create()
        {
            LoadFacilities();

            return View();
        }

        // ==========================
        // CREATE POST
        // ==========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SchoolFacility model)
        {
            long schoolId = GetLoggedInSchoolId();

            bool alreadyExists = _context.SchoolFacilities
                .Any(x =>
                    x.SchoolId == schoolId &&
                    x.FacilityId == model.FacilityId);

            if (alreadyExists)
            {
                ModelState.AddModelError("", "Facility already assigned.");
            }

            if (ModelState.IsValid)
            {
                model.SchoolId = schoolId;

                _context.SchoolFacilities.Add(model);

                _context.SaveChanges();

                TempData["success"] =
                    "Facility added successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadFacilities();

            return View(model);
        }

        // ==========================
        // EDIT GET
        // ==========================

        public IActionResult Edit(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolFacilities
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            LoadFacilities();

            return View(data);
        }

        // ==========================
        // EDIT POST
        // ==========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SchoolFacility model)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolFacilities
                .FirstOrDefault(x =>
                    x.Id == model.Id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            bool duplicateFacility = _context.SchoolFacilities
                .Any(x =>
                    x.Id != model.Id &&
                    x.SchoolId == schoolId &&
                    x.FacilityId == model.FacilityId);

            if (duplicateFacility)
            {
                ModelState.AddModelError("",
                    "Facility already assigned.");
            }

            if (ModelState.IsValid)
            {
                data.FacilityId = model.FacilityId;

                _context.SaveChanges();

                TempData["success"] =
                    "Facility updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadFacilities();

            return View(model);
        }

        // ==========================
        // DELETE GET
        // ==========================

        public IActionResult Delete(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolFacilities
                .Include(x => x.Facility)
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            return View(data);
        }

        // ==========================
        // DELETE POST
        // ==========================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.SchoolFacilities
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            _context.SchoolFacilities.Remove(data);

            _context.SaveChanges();

            TempData["success"] =
                "Facility deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        // ==========================
        // LOAD FACILITIES
        // ==========================

        private void LoadFacilities()
        {
            ViewBag.Facilities = new SelectList(
                _context.Facilities
                    .OrderBy(x => x.FacilityTitle)
                    .ToList(),
                "Id",
                "FacilityTitle"
            );
        }
    }
}

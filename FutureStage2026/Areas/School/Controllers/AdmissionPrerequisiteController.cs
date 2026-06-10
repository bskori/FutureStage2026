using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class AdmissionPrerequisiteController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdmissionPrerequisiteController(ApplicationDbContext context)
        {
            _context = context;
        }

        // INDEX

        public IActionResult Index()
        {
            var data = _context.AdmissionPrerequisites
                .Include(x => x.SchoolStandard)
                .ToList();

            return View(data);
        }

        // CREATE GET

        public IActionResult Create()
        {
            LoadSchoolStandards();
            return View();
        }

        // CREATE POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AdmissionPrerequisite model)
        {
           
            if (ModelState.IsValid)
            {
                _context.AdmissionPrerequisites.Add(model);
                _context.SaveChanges();

                TempData["success"] = "Admission prerequisite added successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadSchoolStandards();
            return View(model);
        }

        // EDIT GET

        public IActionResult Edit(long id)
        {
            var prerequisite = _context.AdmissionPrerequisites
                .FirstOrDefault(x => x.Id == id);

            if (prerequisite == null)
                return NotFound();

            LoadSchoolStandards();

            return View(prerequisite);
        }

        // EDIT POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(AdmissionPrerequisite model)
        {
            if (ModelState.IsValid)
            {
                _context.AdmissionPrerequisites.Update(model);
                _context.SaveChanges();

                TempData["success"] = "Admission prerequisite updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadSchoolStandards();

            return View(model);
        }

        
        // DELETE GET

        public IActionResult Delete(long id)
        {
            var prerequisite = _context.AdmissionPrerequisites
                .Include(x => x.SchoolStandard)
                .FirstOrDefault(x => x.Id == id);

            if (prerequisite == null)
                return NotFound();

            return View(prerequisite);
        }

        // DELETE POST

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(AdmissionPrerequisite model)
        {
            var prerequisite = _context.AdmissionPrerequisites
                .FirstOrDefault(x => x.Id == model.Id);

            if (prerequisite == null)
                return NotFound();

            _context.AdmissionPrerequisites.Remove(prerequisite);
            _context.SaveChanges();

            TempData["success"] = "Admission prerequisite deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        
        // DROPDOWN
        

        private void LoadSchoolStandards()
        {
            var data = _context.SchoolStandards
                .Include(x => x.School)
                .Include(x => x.Standard)
                .Select(x => new
                {
                    x.Id,
                    DisplayText = x.School.Name + " - " +
                                  x.Standard.StandardTitle
                })
                .ToList();

            ViewBag.SchoolStandards =
                new SelectList(data, "Id", "DisplayText");
        }
    }
}

using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class AdmissionProcessController : Controller
    {
       
            private readonly ApplicationDbContext _context;

            public AdmissionProcessController(ApplicationDbContext context)
            {
                _context = context;
            }

            // =====================================
            // INDEX
            // =====================================

            public IActionResult Index()
            {
                var data = _context.AdmissionProcesses
                    .Include(x => x.SchoolStandard)
                    .ToList();

                return View(data);
            }

            // =====================================
            // CREATE GET
            // =====================================

            public IActionResult Create()
            {
                LoadSchoolStandards();

                return View();
            }

            // =====================================
            // CREATE POST
            // =====================================

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(AdmissionProcess model)
            {
                if (ModelState.IsValid)
                {
                    _context.AdmissionProcesses.Add(model);
                    _context.SaveChanges();

                    TempData["success"] = "Admission process added successfully.";

                    return RedirectToAction(nameof(Index));
                }

                LoadSchoolStandards();

                return View(model);
            }

            // =====================================
            // EDIT GET
            // =====================================

            public IActionResult Edit(long id)
            {
                var process = _context.AdmissionProcesses
                    .FirstOrDefault(x => x.Id == id);

                if (process == null)
                    return NotFound();

                LoadSchoolStandards();

                return View(process);
            }

            // =====================================
            // EDIT POST
            // =====================================

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(AdmissionProcess model)
            {
                if (ModelState.IsValid)
                {
                    _context.AdmissionProcesses.Update(model);
                    _context.SaveChanges();

                    TempData["success"] = "Admission process updated successfully.";

                    return RedirectToAction(nameof(Index));
                }

                LoadSchoolStandards();

                return View(model);
            }

            // =====================================
            // DELETE GET
            // =====================================

            public IActionResult Delete(long id)
            {
                var process = _context.AdmissionProcesses
                    .Include(x => x.SchoolStandard)
                    .FirstOrDefault(x => x.Id == id);

                if (process == null)
                    return NotFound();

                return View(process);
            }

            // =====================================
            // DELETE POST
            // =====================================

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Delete(AdmissionProcess model)
            {
                var process = _context.AdmissionProcesses
                    .FirstOrDefault(x => x.Id == model.Id);

                if (process == null)
                    return NotFound();

                _context.AdmissionProcesses.Remove(process);
                _context.SaveChanges();

                TempData["success"] = "Admission process deleted successfully.";

                return RedirectToAction(nameof(Index));
            }

            // =====================================
            // DROPDOWN
            // =====================================

            private void LoadSchoolStandards()
            {
                var data = _context.SchoolStandards
                    .Include(x => x.School)
                    .Include(x => x.Standard)
                    .Select(x => new
                    {
                        x.Id,
                        DisplayText =
                            x.School.Name + " - " +
                            x.Standard.StandardTitle
                    })
                    .ToList();

                ViewBag.SchoolStandards =
                    new SelectList(data, "Id", "DisplayText");
            }
        }
}

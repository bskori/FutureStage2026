using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class StandardSeatQuotaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandardSeatQuotaController(ApplicationDbContext context)
        {
            _context = context;
        }

        private long GetLoggedInSchoolId()
        {
            return Convert.ToInt64(HttpContext.Session.GetString("SchoolId"));
        }

        // =====================================
        // INDEX
        // =====================================

        public IActionResult Index()
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.StandardSeatQuotas
                .Include(x => x.Quota)
                .Include(x => x.SchoolStandard)
                    .ThenInclude(x => x.Standard)
                .Where(x => x.SchoolStandard.SchoolId == schoolId)
                .ToList();

            return View(data);
        }

        // =====================================
        // CREATE GET
        // =====================================

        public IActionResult Create()
        {
            LoadDropdowns();

            return View();
        }

        // =====================================
        // CREATE POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(StandardSeatQuota model)
        {
            long schoolId = GetLoggedInSchoolId();

            bool exists = _context.StandardSeatQuotas
                .Any(x =>
                    x.SchoolStandardId == model.SchoolStandardId &&
                    x.QuotaId == model.QuotaId);

            if (exists)
            {
                ModelState.AddModelError("",
                    "Quota already assigned for this standard.");
            }

            if (ModelState.IsValid)
            {
                _context.StandardSeatQuotas.Add(model);

                _context.SaveChanges();

                TempData["success"] =
                    "Seat quota added successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();

            return View(model);
        }

        // =====================================
        // EDIT GET
        // =====================================

        public IActionResult Edit(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.StandardSeatQuotas
                .Include(x => x.SchoolStandard)
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolStandard.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            LoadDropdowns();

            return View(data);
        }

        // =====================================
        // EDIT POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(StandardSeatQuota model)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.StandardSeatQuotas
                .Include(x => x.SchoolStandard)
                .FirstOrDefault(x =>
                    x.Id == model.Id &&
                    x.SchoolStandard.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            bool duplicate = _context.StandardSeatQuotas
                .Any(x =>
                    x.Id != model.Id &&
                    x.SchoolStandardId == model.SchoolStandardId &&
                    x.QuotaId == model.QuotaId);

            if (duplicate)
            {
                ModelState.AddModelError("",
                    "Quota already assigned for this standard.");
            }

            if (ModelState.IsValid)
            {
                data.SchoolStandardId = model.SchoolStandardId;
                data.QuotaId = model.QuotaId;
                data.NoOfSeats = model.NoOfSeats;

                _context.SaveChanges();

                TempData["success"] =
                    "Seat quota updated successfully.";

                return RedirectToAction(nameof(Index));
            }

            LoadDropdowns();

            return View(model);
        }

        // =====================================
        // DELETE GET
        // =====================================

        public IActionResult Delete(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.StandardSeatQuotas
                .Include(x => x.Quota)
                .Include(x => x.SchoolStandard)
                    .ThenInclude(x => x.Standard)
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolStandard.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            return View(data);
        }

        // =====================================
        // DELETE POST
        // =====================================

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            long schoolId = GetLoggedInSchoolId();

            var data = _context.StandardSeatQuotas
                .Include(x => x.SchoolStandard)
                .FirstOrDefault(x =>
                    x.Id == id &&
                    x.SchoolStandard.SchoolId == schoolId);

            if (data == null)
                return NotFound();

            _context.StandardSeatQuotas.Remove(data);

            _context.SaveChanges();

            TempData["success"] =
                "Seat quota deleted successfully.";

            return RedirectToAction(nameof(Index));
        }

        // =====================================
        // DROPDOWNS
        // =====================================

        private void LoadDropdowns()
        {
            long schoolId = GetLoggedInSchoolId();

            var standards = _context.SchoolStandards
                .Include(x => x.Standard)
                .Where(x => x.SchoolId == schoolId)
                .Select(x => new
                {
                    x.Id,
                    Name = x.Standard.StandardTitle
                })
                .ToList();

            ViewBag.SchoolStandards =
                new SelectList(standards, "Id", "Name");

            ViewBag.Quotas =
                new SelectList(
                    _context.Quotas.OrderBy(x => x.QuotaTitle).ToList(),
                    "Id",
                    "QuotaTitle"
                );
        }
    }
}

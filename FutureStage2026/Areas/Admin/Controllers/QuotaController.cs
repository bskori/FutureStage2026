using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    using global::FutureStage2026.Data;
    using global::FutureStage2026.Models;
    using Microsoft.AspNetCore.Mvc;

    namespace FutureStage2026.Areas.Admin.Controllers
    {
        [Area("Admin")]
        public class QuotaController : Controller
        {
            private readonly ApplicationDbContext _context;

            public QuotaController(ApplicationDbContext context)
            {
                _context = context;
            }

            // LIST
            public IActionResult Index()
            {
                var data = _context.Quotas.ToList();
                return View(data);
            }

            // CREATE GET
            public IActionResult Create()
            {
                return View();
            }

            // CREATE POST
            [HttpPost]
            public IActionResult Create(Quota model)
            {
                if (ModelState.IsValid)
                {
                    _context.Quotas.Add(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            // EDIT GET
            public IActionResult Edit(long id)
            {
                var data = _context.Quotas.Find(id);
                if (data == null)
                    return NotFound();

                return View(data);
            }

            // EDIT POST
            [HttpPost]
            public IActionResult Edit(Quota model)
            {
                if (ModelState.IsValid)
                {
                    _context.Quotas.Update(model);
                    _context.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(model);
            }

            // DELETE 
            public IActionResult Delete(long id)
            {
                var data = _context.Quotas.Find(id);

                if (data == null)
                    return NotFound();

                var isUsed = _context.StandardSeatQuotas.Any(x => x.QuotaId == id);

                if (isUsed)
                {
                    TempData["Error"] = "Quota is already used in seat allocation!";
                    return RedirectToAction("Index");
                }

                _context.Quotas.Remove(data);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }
        }
    }
}

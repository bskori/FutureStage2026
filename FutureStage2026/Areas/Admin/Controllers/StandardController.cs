using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StandardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StandardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var data = _context.Standards.ToList();
            return View(data);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Standard model)
        {
            if (ModelState.IsValid)
            {
                _context.Standards.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // EDIT GET
        public IActionResult Edit(long id)
        {
            var data = _context.Standards.Find(id);
            if (data == null)
                return NotFound();

            return View(data);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Standard model)
        {
            if (ModelState.IsValid)
            {
                _context.Standards.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // DELETE
        public IActionResult Delete(long id)
        {
            var data = _context.Standards.Find(id);

            if (data != null)
            {
                _context.Standards.Remove(data);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

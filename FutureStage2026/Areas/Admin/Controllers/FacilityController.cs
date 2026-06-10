using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FacilityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FacilityController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var data = _context.Facilities.ToList();
            return View(data);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(Facility model)
        {
            if (ModelState.IsValid)
            {
                _context.Facilities.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // EDIT GET
        public IActionResult Edit(long id)
        {
            var data = _context.Facilities.Find(id);
            if (data == null)
                return NotFound();

            return View(data);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(Facility model)
        {
            if (ModelState.IsValid)
            {
                _context.Facilities.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // DELETE
        [HttpGet]
        public IActionResult Delete(long id)
        {
            var data = _context.Facilities.Find(id);
            if (data == null)
            {
                return NotFound();
            }

            return View(data);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var data = _context.Facilities.Find(id);
            if (data != null)
            {
                _context.Facilities.Remove(data);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index");
        }



        // DETAILS 
        public IActionResult Details(long id)
        {
            var data = _context.Facilities.Find(id);
            if (data == null)
                return NotFound();

            return View(data);
        }
    }
}

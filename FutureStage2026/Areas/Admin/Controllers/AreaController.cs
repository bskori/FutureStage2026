using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AreaController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AreaController(ApplicationDbContext context)
        {
            _context = context;
        }
      
        public IActionResult Index()
        {
            var areas = _context.Areas.Include(a => a.City).ThenInclude(c => c.State).ThenInclude(s => s.Country).ToList();
            return View(areas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Cities = _context.Cities
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.CityName
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(Area model)
        {
            if (ModelState.IsValid)
            {
                _context.Areas.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.Cities = _context.Cities
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.CityName
                }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var area = _context.Areas.Find(id);

            if (area == null)
            {
                return NotFound();
            }

            ViewBag.Cities = _context.Cities
               .Select(s => new SelectListItem
               {
                   Value = s.Id.ToString(),
                   Text = s.CityName
               }).ToList();

            return View(area);
        }

        [HttpPost]
        public IActionResult Edit(Area model)
        {
            if (ModelState.IsValid)
            {
                _context.Areas.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Cities = _context.Cities
               .Select(s => new SelectListItem
               {
                   Value = s.Id.ToString(),
                   Text = s.CityName
               }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var area = _context.Areas.Include(a => a.City).ThenInclude(c => c.State).ThenInclude(s => s.Country).FirstOrDefault(a => a.Id == id);

            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var area = _context.Areas.Find(id);

            if (area != null)
            {
                _context.Areas.Remove(area);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(area);
        }
    }
}

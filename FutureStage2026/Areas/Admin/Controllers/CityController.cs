using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CityController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CityController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var cities = _context.Cities.Include(c => c.State).ThenInclude(s => s.Country).ToList();
            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.States = _context.States
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StateName
                }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(City model)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.States = _context.States
                .Select(s => new SelectListItem
                {
                    Value = s.Id.ToString(),
                    Text = s.StateName
                }).ToList();

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var city = _context.Cities.Find(id);

            if (city == null)
            {
                return NotFound();
            }

            ViewBag.States = _context.States.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.StateName
            });

            return View(city);
        }

        [HttpPost]
        public IActionResult Edit(City model)
        {
            if (ModelState.IsValid)
            {
                _context.Cities.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.States = _context.States.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.StateName
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var city = _context.Cities.Include(c=>c.State).ThenInclude(s=>s.Country).FirstOrDefault(c => c.Id == id);

            if (city == null)
            {
                return NotFound();
            }

            return View(city);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var city = _context.Cities.Find(id);

            if (city != null)
            {
                _context.Cities.Remove(city);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(city);
        }
    }
}

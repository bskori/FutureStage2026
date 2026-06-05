using FutureStage2026.Data;
using FutureStage2026.Filters;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[AdminAuthorize]
    public class CountryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CountryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var countries = _context.Countries.ToList();
            return View(countries);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Country model)
        {

            if (ModelState.IsValid)
            {
                _context.Countries.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var country = _context.Countries.Find(id);

            if(country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        public IActionResult Edit(Country model)
        {
            if (ModelState.IsValid)
            {
                _context.Countries.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var country = _context.Countries.Find(id);

            if(country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var country = _context.Countries.Find(id);

            if(country != null)
            {
                _context.Countries.Remove(country);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

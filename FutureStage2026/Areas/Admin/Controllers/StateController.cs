using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class StateController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StateController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var states = _context.States.Include(s => s.Country).ToList();
            return View(states);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            });

            return View();
        }

        [HttpPost]
        public IActionResult Create(State model)
        {

            if (ModelState.IsValid)
            {
                _context.States.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Edit(long id)
        {
            var state = _context.States.Find(id);

            if(state == null)
            {
                return NotFound();
            }

            ViewBag.Countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            });

            return View(state);
        }

        [HttpPost]
        public IActionResult Edit(State model)
        {
            if (ModelState.IsValid)
            {
                _context.States.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Countries = _context.Countries.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            });

            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(long id)
        {
            var state = _context.States.Include(s => s.Country).FirstOrDefault(s => s.Id == id);

            if(state == null)
            {
                return NotFound();
            }

            return View(state);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirmed(long id)
        {
            var state = _context.States.Find(id);

            if(state != null)
            {
                _context.States.Remove(state);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(state);
        }
    }
}

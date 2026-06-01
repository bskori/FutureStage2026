using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutureStage2026.Controllers
{
    public class SchoolController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SchoolController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {

            ViewBag.Areas = _context.Areas.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.AreaName
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(School model) 
        {
            if (ModelState.IsValid)
            {
                _context.Schools.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Create");
            }

            ViewBag.Areas = _context.Areas.Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.AreaName
            });

            return View(model);
        }
    }
}

using FutureStage2026.Data;
using FutureStage2026.Models;
using FutureStage2026.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var school = _context.Schools.FirstOrDefault(x => x.EmailId == model.EmailId && x.PasswordHas == model.Password);

            if(school == null)
            {
                ModelState.AddModelError("", "Invalid Email or Password");
                return View(model);
            }

            HttpContext.Session.SetString("SchoolId", school.Id.ToString());

            return RedirectToAction("Index", "Dashboard", new { area = "School" });
        }
        
    }
}

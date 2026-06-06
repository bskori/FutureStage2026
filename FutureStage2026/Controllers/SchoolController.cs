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

            ViewBag.Countries = _context.Countries
            .Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.CountryName
            }).ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Create(School model, IFormFile ImageFile) 
        {
           

            if (ModelState.IsValid)
            {
                if(ImageFile != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Frontend/Images", fileName);

                    using(var stream = new FileStream(path, FileMode.Create))
                    {
                        ImageFile.CopyTo(stream);
                    }

                    model.ImagePath = "/Frontend/Images/" + fileName;

                }

                _context.Schools.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Login");
            }

            ViewBag.Countries = _context.Countries
             .Select(c => new SelectListItem
             {
                 Value = c.Id.ToString(),
                 Text = c.CountryName
             }).ToList();

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

            var school = _context.Schools.FirstOrDefault(x => x.EmailId == model.EmailId && x.PasswordHash == model.Password);

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

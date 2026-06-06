using FutureStage2026.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.Countries = _context.Countries.Select(c => new SelectListItem
               {
                   Value = c.Id.ToString(),
                   Text = c.CountryName
               }).ToList();

            return View();
        }
    }
}

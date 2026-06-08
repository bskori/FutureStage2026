using FutureStage2026.Data;
using FutureStage2026.Models;
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

            var schools = _context.Schools.Include(s => s.Area).ThenInclude(a => a.City).ToList();


            return View(schools);
        }

        public IActionResult GetSchools(long? countryId, long? stateId, long? cityId, long? areaId)
        {
            var query = _context.Schools
                .Include(s => s.Area)
                    .ThenInclude(a => a.City)
                        .ThenInclude(c => c.State)
                            .ThenInclude(s => s.Country)
                .AsQueryable();

            if (areaId.HasValue)
                query = query.Where(s => s.AreaId == areaId);

            else if (cityId.HasValue)
                query = query.Where(s => s.Area.CityId == cityId);

            else if (stateId.HasValue)
                query = query.Where(s => s.Area.City.StateId == stateId);

            else if (countryId.HasValue)
                query = query.Where(s => s.Area.City.State.CountryId == countryId);

            var schools = query.ToList();

            return PartialView("_SchoolList", schools);
        }

        public IActionResult SchoolDetails(long id)
        {
            var school = _context.Schools
                .Include(s => s.Area)
                .ThenInclude(a => a.City)
                .ThenInclude(c => c.State)
                .ThenInclude(s => s.Country)
                .Include(s => s.Reviews)
                .Include(s => s.EducationBoard)
                .Include(s => s.Medium)
                .Include(s => s.SchoolFacilities)
                .ThenInclude(sf => sf.Facility)
                .FirstOrDefault(s => s.Id == id);

            ViewBag.AvgRating = (school.Reviews != null && school.Reviews.Any())
                ? school.Reviews.Average(r => r.Rating) 
                : 0;

            return View(school);
        }

        [HttpPost]
        public IActionResult AddReview(Review model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("SchoolDetails", new { id = model.SchoolId });
            }

            model.CreatedAt = DateTime.UtcNow;

            _context.Reviews.Add(model);
            _context.SaveChanges();

            return RedirectToAction("SchoolDetails", new { id = model.SchoolId });
        }
    }
}

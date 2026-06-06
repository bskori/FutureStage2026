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
                .FirstOrDefault(s => s.Id == id);

            return View(school);
        }
    }
}

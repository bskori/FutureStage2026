using FutureStage2026.Data;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Controllers
{
    public class LocationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public LocationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public JsonResult GetStates(long countryId)
        {
            var states = _context.States.Where(s => s.CountryId == countryId).Select(s => new
            {
                id = s.Id,
                name = s.StateName
            }).ToList();
            return Json(states);
        }

        [HttpGet]
        public JsonResult GetCities(long stateId)
        {
            var cities = _context.Cities.Where(c => c.StateId == stateId).Select(c => new
            {
                id = c.Id,
                name = c.CityName
            }).ToList();

            return Json(cities);
        }

        [HttpGet]
        public JsonResult GetAreas(long cityId)
        {
            var areas = _context.Areas.Where(c => c.CityId == cityId).Select(c => new
            {
                id = c.Id,
                name = c.AreaName
            }).ToList();

            return Json(areas);
        }
    }
}

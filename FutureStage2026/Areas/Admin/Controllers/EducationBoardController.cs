using FutureStage2026.Data;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationBoardController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EducationBoardController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LIST
        public IActionResult Index()
        {
            var boards = _context.EducationBoards.ToList();
            return View(boards);
        }

        // CREATE GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE POST
        [HttpPost]
        public IActionResult Create(EducationBoard model)
        {
            if (ModelState.IsValid)
            {
                _context.EducationBoards.Add(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // EDIT GET
        public IActionResult Edit(long id)
        {
            var board = _context.EducationBoards.Find(id);

            if (board == null)
                return NotFound();

            return View(board);
        }

        // EDIT POST
        [HttpPost]
        public IActionResult Edit(EducationBoard model)
        {
            if (ModelState.IsValid)
            {
                _context.EducationBoards.Update(model);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        // DELETE
        public IActionResult Delete(long id)
        {
            var board = _context.EducationBoards.Find(id);

            if (board != null)
            {
                _context.EducationBoards.Remove(board);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}

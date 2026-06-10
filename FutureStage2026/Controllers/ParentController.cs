using Microsoft.AspNetCore.Mvc;
using FutureStage2026.Data;
using FutureStage2026.Models;
using FutureStage2026.ViewModels;
using System.Linq;
using Microsoft.EntityFrameworkCore;

public class ParentController : Controller
{
    private readonly ApplicationDbContext _context;

    public ParentController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Dashboard()
    {
        var parentIdStr = HttpContext.Session.GetString("ParentId");

        if (string.IsNullOrEmpty(parentIdStr))
        {
            return RedirectToAction("Login");
        }

        var parentId = Convert.ToInt64(parentIdStr);

        var data = _context.Enquiries
            .Where(x => x.ParentId == parentId)
            .Include(x => x.School)
            .Include(x => x.SchoolStandard)
                .ThenInclude(x => x.Standard)
            .ToList();

        return View(data);
    }

    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var parent = new Parent
            {
                Name = model.Name,
                Address = model.Address,
                EmailId = model.EmailId,
                MobileNo = model.MobileNo,
                PasswordHash = model.Password 
            };

            _context.Parents.Add(parent);
            _context.SaveChanges();

            return RedirectToAction("Login");
        }

        return View(model);
    }

    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            
            var parent = _context.Parents
                .FirstOrDefault(x => x.EmailId == model.EmailId
                                  && x.PasswordHash == model.Password);

            if (parent != null)
            {
                HttpContext.Session.SetString("ParentId", parent.Id.ToString());
                HttpContext.Session.SetString("ParentName", parent.Name);

                return RedirectToAction("Dashboard");
            }

            ModelState.AddModelError("", "Invalid Email or Password");
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
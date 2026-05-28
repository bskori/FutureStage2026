using Microsoft.AspNetCore.Mvc;
using FutureStage2026.Data;
using FutureStage2026.Models;
using FutureStage2026.ViewModels;
using System.Linq;

public class AccountController : Controller
{
    private readonly ApplicationDbContext _context;

    public AccountController(ApplicationDbContext context)
    {
        _context = context;
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
            var user = _context.Parents
                .FirstOrDefault(x => x.EmailId == model.EmailId
                                  && x.PasswordHash == model.Password);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserName", user.Name);

                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError("", "Invalid login");
        }

        return View(model);
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
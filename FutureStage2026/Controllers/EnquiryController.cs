using FutureStage2026.Data;
using FutureStage2026.Enums;
using FutureStage2026.Models;
using FutureStage2026.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FutureStage2026.Controllers
{
    public class EnquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnquiryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Create(long schoolId)
        {
            var vm = new EnquiryVM
            {
                SchoolId = schoolId
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(EnquiryVM vm)
        {
            if (ModelState.IsValid)
            {
                var parent = new Parent
                {
                    Name = vm.ParentName,
                    EmailId = vm.Email,
                    MobileNo = vm.MobileNo,
                    Address = vm.Address,
                    PasswordHash = "123456" 
                };

                _context.Parents.Add(parent);
                _context.SaveChanges();

                var enquiry = new Enquiry
                {
                    SchoolId = vm.SchoolId,
                    ParentId = parent.Id,
                    EnquiryDesc = vm.EnquiryDesc,
                    EnquiryType = EnquiryType.Admission,
                    EnquiryDate = DateTime.Now
                };

                _context.Enquiries.Add(enquiry);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return View(vm);
        }
    }
}

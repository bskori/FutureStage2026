using FutureStage2026.Data;
using FutureStage2026.Enums;
using FutureStage2026.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FutureStage2026.Areas.School.Controllers
{
    [Area("School")]
    public class EnquiryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EnquiryController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var schoolId = Convert.ToInt64(HttpContext.Session.GetString("SchoolId"));

            var data = _context.Enquiries
              .Where(x => x.SchoolId == schoolId)
               .Include(x => x.Parent)
               .Include(x => x.SchoolStandard)
              .ThenInclude(x => x.Standard)
              .ToList();

            return View(data);
        }

        [HttpGet]
        public IActionResult Reply(long id)
        {
            var enquiry = _context.Enquiries.Find(id);
            return View(enquiry);
        }

        [HttpPost]
        public IActionResult Reply(long enquiryId, string response)
        {
            var reply = new EnquiryReply
            {
                EnquiryId = enquiryId,
                ReplyDesc = response,
                ReplyDate = DateTime.Now
            };

            _context.EnquiryReplies.Add(reply);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult UpdateStatus(long enquiryId, string status)
        {
            var enquiry = _context.Enquiries.FirstOrDefault(x => x.Id == enquiryId);

            if (enquiry == null)
                return NotFound();

            if (status == "accept")
            {
                enquiry.AdmissionStatus = AdmissionStatus.Approved;
                enquiry.ConfirmationDate = DateTime.Now;
            }
            else if (status == "reject")
            {
                enquiry.AdmissionStatus = AdmissionStatus.Rejected;
            }

            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}

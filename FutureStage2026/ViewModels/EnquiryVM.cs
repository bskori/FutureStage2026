using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.ViewModels
{
    public class EnquiryVM
    {
        public long SchoolId { get; set; }

        [Required]
        public string ParentName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string MobileNo { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string EnquiryDesc { get; set; }
    }
}

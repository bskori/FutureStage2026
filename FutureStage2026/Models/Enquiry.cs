using FutureStage2026.Enums;
using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Enquiry : BaseEntity
    {
        [Required]
        public string EnquiryDesc { get; set; }

        public DateTime EnquiryDate { get; set; } = DateTime.UtcNow;

        public long ParentId { get; set; }
        public Parent Parent { get; set; }

        public long SchoolId { get; set; }
        public School School { get; set; }

        [Required]
        public EnquiryType EnquiryType { get; set; }

        public long? SchoolStandardId { get; set; }
        public SchoolStandard SchoolStandard { get; set; }

        public AdmissionStatus AdmissionStatus { get; set; }

        public DateTime? ConfirmationDate { get; set; }

        public ICollection<EnquiryReply> EnquiryReplies { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class EnquiryReply : BaseEntity
    {
        [Required]
        public long EnquiryId { get; set; }

        public Enquiry Enquiry { get; set; }

        [Required]
        [StringLength(500)]
        public string ReplyDesc { get; set; }

        public DateTime ReplyDate { get; set; } = DateTime.UtcNow;
    }
}

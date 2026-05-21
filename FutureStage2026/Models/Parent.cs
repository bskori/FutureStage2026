using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Parent : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(250)]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string EmailId { get; set; }

        [Required]
        [Phone]
        public string MobileNo { get; set; }

        [Required]
        public string PasswordHash { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }
    }
}
using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class School : BaseEntity
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
        public string ContactNo { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string PasswordHas { get; set; }

        [Required]
        public long AreaId { get; set; }

        public Area? Area { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EstablishmentDate { get; set; }

        public ICollection<SchoolStandard>? SchoolStandards { get; set; }
        public ICollection<SchoolFacility>? SchoolFacilities { get; set; }
        public ICollection<SchoolAchievement>? SchoolAchievements { get; set; }
        public ICollection<Enquiry>? Enquiries { get; set; }

        public ICollection<StandardFees>? StandardFees { get; set; }
    }
}

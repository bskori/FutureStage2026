using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Facility: BaseEntity
    {

        [Required]
        [StringLength(100)]
        public string FacilityTitle { get; set; }

        [StringLength(250)]
        public string? FacilityDesc { get; set; }
        public ICollection<SchoolFacility> SchoolFacilities { get; set; }
    }
}

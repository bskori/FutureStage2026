using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class SchoolFacility : BaseEntity
    {

        [Required]
        public long SchoolId { get; set; }

        public School? School { get; set; }

        [Required]
        public long FacilityId { get; set; }

        public Facility? Facility { get; set; }

        
    }
}

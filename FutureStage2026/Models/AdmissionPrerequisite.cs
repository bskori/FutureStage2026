using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class AdmissionPrerequisite :BaseEntity
    {
        [Required]
        [StringLength(150)]
        public string PrerequisiteTitle { get; set; }

        [Required]
        public long SchoolStandardId { get; set; }

        public SchoolStandard? SchoolStandard { get; set; }

        [StringLength(500)]
        public string PrerequisiteDesc { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class AdmissionProcess :BaseEntity
    {
        [Required]
        public long SchoolStandardId { get; set; }

        public SchoolStandard? SchoolStandard { get; set; }

        [Required]
        [StringLength(150)]
        public string ProcessTitle { get; set; }

        [StringLength(500)]
        public string ProcessDesc { get; set; }
    }
}

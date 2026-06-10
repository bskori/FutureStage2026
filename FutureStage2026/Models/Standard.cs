using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Standard : BaseEntity
    {

        [Required]
        [StringLength(50)]

        public string StandardTitle { get; set; }

        public string StandardDesc { get; set; }

        public ICollection<SchoolStandard>? SchoolStandards { get; set; }
    }
}

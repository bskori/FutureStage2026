using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class StandardFees : BaseEntity
    {

        [Required]
        public long SchoolStandardId { get; set; }
        public SchoolStandard? SchoolStandard { get; set; }

        [Required]
        public long FeeHeadId { get; set; }
        public FeeHead? FeeHead { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Amount { get; set; }
    }
}

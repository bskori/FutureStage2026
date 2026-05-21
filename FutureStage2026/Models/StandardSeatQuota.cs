using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class StandardSeatQuota : BaseEntity
    {

        [Required]
        public long SchoolStandardId { get; set; }

        public SchoolStandard SchoolStandard { get; set; }

        [Required]
        public long QuotaId { get; set; }

        public Quota Quota { get; set; }

        [Range(0, 10000)]
        public int NoOfSeats { get; set; }
    }
}

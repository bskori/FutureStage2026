using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Quota : BaseEntity
    {

        [Required]
        [StringLength(100)]
        public string QuotaTitle { get; set; }

        [StringLength(250)]
        public string QuotaDesc { get; set; }

        public ICollection<StandardSeatQuota>? StandardSeatQuotas { get; set; }
    }
}

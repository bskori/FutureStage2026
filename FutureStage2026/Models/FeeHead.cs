using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class FeeHead : BaseEntity
    {

        [Required]
        [StringLength(100)]
        public string FeeHeadTitle { get; set; }

        [StringLength(250)]
        public string FeeHeadDesc { get; set; }
        public ICollection<StandardFees> StandardFees { get; set; }
    }
}

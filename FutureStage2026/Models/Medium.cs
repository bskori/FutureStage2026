using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Medium : BaseEntity
    {

        [Required]
        [StringLength(50)]
        public string MediumTitle { get; set; }
    }
}

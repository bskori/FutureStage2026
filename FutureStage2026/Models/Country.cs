using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Country : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string CountryName { get; set; }

        public ICollection<State> States { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class State : BaseEntity
    {

        [Required]
        public string StateName { get; set; }

        [Required]
        public long CountryId { get; set; }
        public Country Country { get; set; }

        public ICollection<City> Cities { get; set; }
    }
}

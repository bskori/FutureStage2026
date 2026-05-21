using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class City :BaseEntity
    {
        [Required]
        public string CityName { get; set; }

        [Required]
        public long StateId { get; set; }

        public State State { get; set; }

        public ICollection<Area> Areas { get; set; }
    }
}

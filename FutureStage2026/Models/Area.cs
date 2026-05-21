using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Area : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string AreaName { get; set; }

        [Required]
        public long CityId { get; set; }

        public City City { get; set; }

        public ICollection<School> Schools { get; set; }
    }
}

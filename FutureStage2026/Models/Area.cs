using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

        [ValidateNever]
        public City City { get; set; }

        [ValidateNever]
        public ICollection<School> Schools { get; set; }
    }
}

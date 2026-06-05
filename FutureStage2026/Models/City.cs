using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class City :BaseEntity
    {
        [Required]
        public string CityName { get; set; }

        [Required]
        public long StateId { get; set; }

        [ValidateNever]
        public State State { get; set; }

        [ValidateNever]
        public ICollection<Area> Areas { get; set; }
    }
}

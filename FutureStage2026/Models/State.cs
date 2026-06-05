using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class State : BaseEntity
    {

        [Required]
        public string StateName { get; set; }

        [Required]
        public long CountryId { get; set; }

        [ValidateNever]
        public Country Country { get; set; }

        [ValidateNever]
        public ICollection<City> Cities { get; set; }
    }
}

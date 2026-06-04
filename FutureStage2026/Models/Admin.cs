using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class Admin:BaseEntity
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class SchoolAchievement : BaseEntity
    {
        [Required]
        public DateTime SchoolAchievementDate { get; set; }

        [Required]
        [StringLength(150)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Desc { get; set; }

        [Required]
        public long SchoolId { get; set; }

        public School? School { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class EducationBoard : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string EducationBoardTitle { get; set; }
    }
}

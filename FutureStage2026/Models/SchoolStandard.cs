using System.ComponentModel.DataAnnotations;

namespace FutureStage2026.Models
{
    public class SchoolStandard : BaseEntity
    {

        [Required]
        public long SchoolId { get; set; }

        public School School { get; set; }

        [Required]
        public long StandardId { get; set; }

        public Standard Standard { get; set; }

        [Range(1, 10000)]
        public int IntakeCapacity { get; set; }

        public ICollection<StandardSeatQuota> StandardSeatQuotas { get; set; }
        public ICollection<StandardFees> StandardFees { get; set; }
        public ICollection<AdmissionPrerequisite> AdmissionPrerequisites { get; set; }
        public ICollection<AdmissionProcess> AdmissionProcesses { get; set; }
        public ICollection<Enquiry> Enquiries { get; set; }
    }
}

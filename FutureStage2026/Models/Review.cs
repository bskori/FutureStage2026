namespace FutureStage2026.Models
{
    public class Review : BaseEntity
    {
        public long SchoolId { get; set; }
        public School School { get; set; }

        public string UserName { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}

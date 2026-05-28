using FutureStage2026.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;

namespace FutureStage2026.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AdmissionPrerequisite> AdmissionPrerequisites { get; set; }
        public DbSet<AdmissionProcess> AdmissionProcesses { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<BaseEntity> BaseEntities { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<EducationBoard> EducationBoards { get; set; }
        public DbSet<Enquiry> Enquiries { get; set; }
        public DbSet<EnquiryReply> EnquiryReplies { get; set; }
        public DbSet<Facility> Facilities { get; set; }
        public DbSet<FeeHead> FeeHeads { get; set; }
        public DbSet<Medium> Mediums { get; set; }
        public DbSet<Parent> Parents { get; set; }
        public DbSet<Quota> Quotas { get; set; }
        public DbSet<School> Schools { get; set; }
        public DbSet<SchoolAchievement> SchoolAchievements { get; set; }
        public DbSet<SchoolFacility> SchoolFacilities { get; set; }
        public DbSet<SchoolStandard> SchoolStandards { get; set; }
        public DbSet<Standard> Standards { get; set; }
        public DbSet<StandardFees> StandardFees { get; set; }
        public DbSet<StandardSeatQuota> StandardSeatQuotas { get; set; }
        public DbSet<State> States { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Ignore<BaseEntity>();

             modelBuilder.Entity<StandardFees>().Property(x => x.Amount).HasPrecision(18, 2);
        }
    }
}

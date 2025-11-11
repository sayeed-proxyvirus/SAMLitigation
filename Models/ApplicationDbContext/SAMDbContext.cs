using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ViewModel;

namespace SAMLitigation.Models.ApplicationDbContext
{
    public class SAMDbContext:DbContext
    {
        public SAMDbContext(DbContextOptions<SAMDbContext> options): base(options)
        {
            
        }

        public DbSet<UserTable> UsersTable { get; set; }
        public DbSet<UserRoleRelationViewModel> UserRoleViewModel { get; set; }
        public DbSet<SAM_Litigation_Lawyer> Lawyer { get; set; }
        public DbSet<SAM_Litigation_Court> Court { get; set; }
        public DbSet<SAM_Litigation_Cause> Cause { get; set; }
        public DbSet<SAM_Litigation_Status> Status { get; set; }
        public DbSet<SAM_Litigation_On> On { get; set; }
        public DbSet<SAM_Litigation_Type> Type { get; set; }
        public DbSet<Loan_NC_T_Sector> Sector { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoleRelationViewModel>().HasNoKey();
            modelBuilder.Entity<Loan_NC_T_ProjectType>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ProjectTypeName).IsRequired().HasMaxLength(100);
                entity.HasOne(e => e.Sector)
                    .WithMany(s => s.Sectors)
                    .HasForeignKey(e => e.SectorId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}

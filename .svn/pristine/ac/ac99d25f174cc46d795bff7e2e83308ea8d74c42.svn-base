using Microsoft.EntityFrameworkCore;
using SAMLitigation.Models.ViewModel;
namespace SAMLitigation.Models.ApplicationDbContext
{
    public class SAMDbContext : DbContext
    {
        public SAMDbContext(DbContextOptions<SAMDbContext> options) : base(options)
        {

        }
        public DbSet<UserTable> UsersTable { get; set; }
        public DbSet<UserRoleRelationViewModel> UserRoleViewModel { get; set; }
        public DbSet<DashboardViewModel> DashboardViewModels { get; set; }
        public DbSet<LoanNCPaperProcessingProjectViewModel> ProjectViewModel { get; set; }
        public DbSet<SAM_Litigation_PartyViewModel> LitigationPartyViewModel { get; set; }
        public DbSet<SAM_Litigation_Lawyer> Lawyer { get; set; }
        public DbSet<SAM_Litigation_Court> Court { get; set; }
        public DbSet<SAM_Litigation_Master> LitigationMaster { get; set; }
        public DbSet<SAM_Litigation_Cause> Cause { get; set; }
        public DbSet<SAM_Litigation_Status> Status { get; set; }
        public DbSet<SAM_Litigation_On> On { get; set; }
        public DbSet<SAM_Litigation_Type> Type { get; set; }
        public DbSet<SAM_Litigation_Party> LitigationParty { get; set; }
        public DbSet<Loan_NC_T_Sector> Sector { get; set; }
        public DbSet<Loan_NC_T_ProjectType> ProjectType { get; set; }
        public DbSet<Loan_NC_PaperProcessing_Cycle> PaperProcessing_Cycles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRoleRelationViewModel>().HasNoKey();

            modelBuilder.Entity<DashboardViewModel>().HasNoKey();

            modelBuilder.Entity<LoanNCPaperProcessingProjectViewModel>().HasNoKey();
            modelBuilder.Entity<SAM_Litigation_PartyViewModel>().HasNoKey();



        }
    }
}
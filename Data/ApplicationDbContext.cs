using Microsoft.EntityFrameworkCore;

namespace WimMed.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
        // DbSet for Patient entity
        public DbSet<Models.Entities.Patient> Patients { get; set; }
        // DbSet for PatientInfo entity
        public DbSet<Models.Entities.PatientInfo> PatientInfos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the Patient entity
            modelBuilder.Entity<Models.Entities.Patient>()
                .HasKey(p => p.Id);
            // Configure the PatientInfo entity
            modelBuilder.Entity<Models.Entities.PatientInfo>()
                .HasKey(pi => pi.Id);



        }
    }
}

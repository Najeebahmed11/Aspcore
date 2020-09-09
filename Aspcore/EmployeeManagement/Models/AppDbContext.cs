
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;
//using System.Data.Entity;
namespace EmployeeManagement.Models
{
    public class AppDbContext: IdentityDbContext<ApplicationUser>
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<License> Licenses { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<UserQualfClaim> UserQualfClaims { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Company> Companies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<UserQualfClaim>()
                        .HasKey(u => new { u.UserId, u.QualificationId });
            modelBuilder.Entity<Company>()
            .HasKey(u => new { u.VehicleGuid, u.CompanyGuid });
            base.OnModelCreating(modelBuilder);
            // modelBuilder.Seed(); Seed will be a method in ModelBuilderExtensions class


            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
        public DbSet<EmployeeManagement.ViewModels.QualificationViewModel> QualificationViewModel { get; set; }
        public DbSet<EmployeeManagement.ViewModels.CreateUserQualificationClaimViewModel> CreateUserQualificationClaimViewModel { get; set; }
    }
}

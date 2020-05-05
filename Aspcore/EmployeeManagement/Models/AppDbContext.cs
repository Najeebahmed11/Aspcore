using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
namespace EmployeeManagement.Models
{
    public class AppDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
    }
}

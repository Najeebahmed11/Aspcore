
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
namespace EmployeeManagement.Models
{
    public class AppDbContext: Microsoft.EntityFrameworkCore.DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options)
            :base(options)
        {

        }
        public Microsoft.EntityFrameworkCore.DbSet<Employee> Employees { get; set; }
    }
}
